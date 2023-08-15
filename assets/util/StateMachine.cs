using Godot;
using System;

public partial class StateMachine<TState> : Node where TState : Enum
{
    private TState state;
    public TState State
    {
        get { return state; }
        set
        {
            TState oldState = state;
            TState newState = value;

            _Enter(oldState, newState);
            state = _ChangeState(oldState, newState);
            _Exit(oldState, newState);
        }
    }

    public virtual void _Enter(TState oldstate, TState newState) { }

    public virtual TState _ChangeState(TState oldState, TState newState)
    {
        return newState;
    }

    public virtual void _Exit(TState oldstate, TState newState) { }
}