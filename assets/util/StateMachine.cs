using Godot;
using System;

public partial class StateMachine<TState> : Node where TState : Enum
{
    private TState state; 
    public TState State {
        get { return state; } 
        private set 
        {
            state = _ChangeState(state, value);
        }
    }

    public TState _ChangeState(TState oldState, TState newState) {
        

        return newState;
    }
}