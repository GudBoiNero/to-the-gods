using Godot;
using System;

public partial class StateMachine<TState> : Node where TState : Enum
{
    private TState state;
    private TState previousState;
    public TState State
    {
        get { return state; }
        set
        {
            TState oldState = state;
            TState newState = value;

            previousState = oldState;

            _Enter(oldState, newState);
            state = _ChangeState(oldState, newState);
            _Exit(oldState, newState);
        }
    }
    public TState PreviousState {
        get { return previousState; }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        State = _StatePhysicsProcess(delta, previousState, state);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        State = _StateProcess(delta, previousState, state);
    }

    public virtual TState _StateProcess(double delta, TState oldstate, TState newstate) {
        return newstate;
    }

    public virtual TState _StatePhysicsProcess(double delta, TState oldstate, TState newstate) {
        return newstate;
    }

    public virtual void _Enter(TState oldstate, TState newState) { }

    public virtual TState _ChangeState(TState oldState, TState newState)
    {
        return newState;
    }

    public virtual void _Exit(TState oldstate, TState newState) { }
}