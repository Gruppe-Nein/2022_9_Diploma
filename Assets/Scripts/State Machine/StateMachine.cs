using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    //public List<State> states = new List<State>();
    public State CurrentState { get; private set; }
    //protected PlayerData playerData;
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
