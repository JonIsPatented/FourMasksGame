
# Documentation for the Movement State Machine Module

## API Goal

The goal of the movement state machine API is to extract one entity's movement state into an enclosed state machine. The owner of the state machine has the responsibility of informing the state machine and interpreting the state machine's output. 

## Usage

To use a movement state machine, define a field to store the state machine and populate it with the constructor. When the entity is ready to have a movement state, enter a default movement state with `EnterState`. The entity should call the `Process` method frequently to allow transitions within the state machine.

### Input and Output

A movement state machine takes in an `Info` and outputs a `Directive`. The `Info` is useful to give the state machine information about the entity's physical circumstances, for example its velocity or its contact with surfaces or objects. The `Directive` describes how the entity should behave given its movement state.

The `Info` and `Directive` for a particular use of the movement state machine should be defined as two structs: one for all data from entity to state and one for all data from state to entity.

The movement state machine stores an internal `Info` that is passed to the states in every interaction. The `PassInfo` method is used to update this `Info`.

The `GetDirective` method returns the directive specified by the current state for the state machine, or the default `Directive` if there is no state.

### State Transitions

When `Process` is called, the state machine checks if any of the future states from the `FutureState` can enter, and if they can, if the current state should exit to them. If both conditions resolve to true, a transition occurs and the current state ends. No more than one transition occurs in each `Process`.

If a state transition occurred during a `Process` call to a movement state machine, the field `TransitionOnLastProcess` will be true until the next `Process` call. Otherwise it will be false. This field can be useful to react to transitions.

If a state's `Directive` makes a request that fails or is invalid, it is possible to immediately move to a different state with the `EscapeState` method.

### Implementing States

States are implemented by defining an implementor of the `MovementState` interface.

All state methods are passed a copy of the `Info` struct contained in their state machine on every call.

A movement state must at least define a list of future state candidates and a method to output a directive.

The `Label` field is used to collect a label value that identifies a state instance. This value is used in `OnExit`, `ShouldExitTo`, and the `GetLabel` method of the movement state machine class.

States can implement their own entry conditions through `CanEnter`. If `CanEnter` returns false, the state will not be entered automatically, though it can still be entered through `CanEnter`.

States can also implement their own exit conditions through `ShouldExitTo`. `ShouldExitTo` receives both the `Info` and the label value of the possible future state. `ShouldExitTo` is only called if the future state has returned true for `CanEnter`. If `ShouldExitTo` returns false, the state will not be exited automatically, though it can still be exited through `EscapeState` which does not call the `ShouldExitTo` method.

States can implement entry and exit effects in the `OnEnter` and `OnExit` methods. The `OnExit` method is notified of the future state selected via a label. Every time the state machine processes, the `Process` method will be called for the current state.
