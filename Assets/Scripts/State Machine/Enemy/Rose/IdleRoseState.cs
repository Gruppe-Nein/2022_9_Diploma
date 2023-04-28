using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRoseState : State
{
    private bool PiggybankDestroyed;
    public IdleRoseState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        PiggybankDestroyed = (brain as RoseBrain).PyggyDestroyed;
        
        if(PiggybankDestroyed)
        {
            stateMachine.ChangeState((brain as RoseBrain).GrabPiggybankState);
        }
    }
}
