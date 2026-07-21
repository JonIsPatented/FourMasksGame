
# Documentation for the InputManager Module

## API Goal

The goal of the input manager API is to allow game classes to access player inputs through a singleton that "washes" input events as they occur based on the input context.

## Usage

To use the input manager from any script, access the autoload instance with `InputManager.Instance`.

The `AimWithJoystick` field determines whether the aim actions or the mouse position will be used to determine the aim input.

### Querying Input

To query if an action was just pressed, call `GetActionPressed` with the action name in the input map. If you would like this method to return true if the action was pressed within more than one frame, pass an optional "buffer value" that is the number of additional frames 

To query if an action is currently held, call `GetActionHeld` with the action name in the input map. If you would like this method to return true if the action was held within more than one frame (so if it was released within a few frames, still return true), pass an optional "buffer value" as for `GetActionPressed`.

The method `GetHorizontalAxis` returns the current horizontal axis on any frame, which is a floating point value from -1 (left) to 1 (right).

The method `GetLastHorizontalAxis` returns the most recent horizontal axis value in `{-1, 1}`, which is useful for deciding which direction to use when the player is not currently motivating the horizontal axis.

A change in the last horizontal axis value can be simulated with `SetDefaultLastHorizontalAxis`, which is useful when placing the player in a new area, for example.

The method `GetAim` returns the current aim value of the player depending on the aim configuration from `AimWithJoystick`. The aim origin is the position on the screen that the mouse position is aiming "from". The aim origin is not used when aiming with actions.

>[!WARNING] The Action Records Table
> The InputManager uses a hard-coded starting value for its action records table that determines which actions it records events for.
> If you query for an action that is not included in this action records table in the class, the input manager will never return that it was pressed.

### Managing Context

The input manager determines which inputs it records using the input context. The input contexts are managed in a context stack, meaning a new context can be pushed over an old context when needed and popped to return regardless of the old context.

By default, the input manager uses a Cutscene input context. This means that scripts that use the input manager for input will not be informed of any inputs from the player. In order for the InputManager to pass inputs normally, a normal input context must be pushed to the context stack.

To push a new context, use `PushContext`. The new context will take effect immediately.

To pop a context, use `PopContext`. It's not possible to replace the bottom of the stack with `PopContext`, to do that use `ClearContext`.

The input manager uses three fields from the input context object: the `useAny` field, which determines if any inputs are recorded, the `useHorizontal` field, which determines if the horizontal axis is recorded, and the `useAim` field, which determines if the aim input is passed through (the aim input is not recorded).

>[!WARNING] The Default Context
> The input manager uses an input context that accepts no inputs by default. The `InputContextSwitchNode` can be used to give a scene a different context by default.
