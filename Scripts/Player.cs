using Godot;
using FourMasksGame.Scripts.Movement;
using FourMasksGame.Scripts.Input;
using FourMasksGame.Scripts.Abilities;
using FourMasksGame.Scripts.Visual;
using FourMasksGame.Scripts.Enemies;
using FourMasksGame.Scripts.Damage;
using System.Collections.Generic;

namespace FourMasksGame.Scripts;

public partial class Player : CharacterBody2D
{
    private MovementStateMachine movementStateMachine;
    private AbilityBridge abilityBridge;
    private HealthBar healthBar = new HealthBar(10f, 10f, true);

    [Export] private AnimatedSprite2D sprite;
    [Export] private SpriteFramesSet spriteSet;
    [Export] private DamageReceiver damageReceiver;

    private SpriteController spriteController;

    private float groundedTime = -1000f;
    private bool needsJump = false;

    // Used for debug.
    public MovementStateMachine StateMachine { get => movementStateMachine; }

    // I don't think export variables are set until after _EnterTree and _Ready, so be careful about using exported node references here.
    public override void _EnterTree()
    {
        movementStateMachine = new();
        movementStateMachine.EnterState(new Movement.States.IdleMovementState());
        abilityBridge = new();
        spriteController = new();
        spriteController.Bind(sprite, spriteSet);
        healthBar.Start();
    }

    public override void _Process(double delta)
    {
        if (IsOnFloor())
        {
            groundedTime = Time.GetTicksMsec() / 1000f;
        }

        if (!healthBar.IsAlive())
        {
            return;
        }

        HashSet<DamageSource> damageSources = damageReceiver.Receive(1);
        if (damageSources.Count > 0)
        {
            spriteController.Damage();
            healthBar.Damage(damageSources);
        }

        if (!healthBar.IsAlive())
        {
            spriteController.Die();
            return;
        }

        if (abilityBridge.UsingAbility())
        {
            // allow chains and ending
            bool continuedAbility = abilityBridge.ContinueAbility();
            if (!continuedAbility)
            {
                movementStateMachine.EscapeState();
            }
        }

        movementStateMachine.PassInfo(new()
        {
            grounded = IsOnFloor(),
            groundedTime = groundedTime,
            realVelocity = Velocity,
            usingAbility = abilityBridge.UsingAbility(),
        });
        movementStateMachine.Process();

        if (movementStateMachine.TransitionOnLastProcess)
        {
            MovementDirective directive = movementStateMachine.GetDirective();
            if (directive.useAbility)
            {
                // TODO: When switching to a state that uses a different ability, end the first

                bool abilityUsed = abilityBridge.UseAbility(this, directive.abilitySlot);
                if (!abilityUsed)
                {
                    movementStateMachine.EscapeState(); // ability-motivating state failed, so escape to a viable state.
                }
            }
            else
            {
                if (abilityBridge.UsingAbility())
                {
                    abilityBridge.EndAbility();
                }
            }

            if (directive.impulseOnEnter != Vector2.Zero)
            {
                needsJump = true;
            }
        }

        if (sprite != null)
        {
            sprite.FlipH = InputManager.Instance.GetLastHorizontalAxis() < 0f;
            sprite.Visible = !movementStateMachine.GetDirective().useAbility;
            switch (movementStateMachine.GetLabel())
            {
                case MovementStateLabel.Idle:
                    spriteController.Do("Idle");
                    break;
                case MovementStateLabel.Run:
                    spriteController.Do("Walk");
                    break;
                case MovementStateLabel.Jump:
                    spriteController.Do("Jump");
                    break;
                case MovementStateLabel.Falling:
                    spriteController.Do("Fall");
                    break;
                default:
                    spriteController.Stop();
                    break;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        MovementDirective directive = movementStateMachine.GetDirective();
        if (directive.useAbilityDirective)
        {
            PhysicsFollowAbilityDirective(abilityBridge.GetDirective(), delta);
        }
        else
        {
            PhysicsFollowMovementDirective(directive, delta);
        }
        MoveAndSlide();
    }

    public void PhysicsFollowMovementDirective(MovementDirective directive, double delta)
    {
        // For a character body, the Velocity property actually represents a requested velocity.
        // Without a reset, applications like gravity will pile up forever.
        // So I'm resetting each step here to the real velocity.
        // That means that the requested Y velocity will stop accumulating if on the ground.
        //Velocity = GetRealVelocity();

        Vector2 _v = Velocity;

        if (needsJump)
        {
            _v = directive.impulseOnEnter;
            needsJump = false;
        }

        if (_v.Y < 0f && !directive.useJumpGravity)
        {
            _v.Y = 0f;
        }

        _v.Y += CustomGravity(directive.useJumpGravity) * (float)delta;

        // The player controls horizontal velocity directive determines whether the player gets to set the horizontal velocity with input.
        if (directive.playerControlsHorizontalVelocity)
        {
            _v.X = directive.horizontalMovementSpeed * InputManager.Instance.GetHorizontalAxis();
        }

        Velocity = _v;
    }

    private float CustomGravity(bool useJumpGravity = false)
    {
        if (useJumpGravity)
        {
            return Constants.PLAYER_RISE_GRAVITY;
        }
        else
        {
            return Constants.PLAYER_FALL_GRAVITY;
        }
    }

    public void PhysicsFollowAbilityDirective(AbilityDirective directive, double delta)
    {
        Vector2 _v = Velocity;
        _v.Y += CustomGravity() * (float)delta;

        if (directive.fixY)
        {
            _v.Y = 0;
        }

        Velocity = _v;
    }
}
