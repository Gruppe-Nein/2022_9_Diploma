using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class IdleRoseState : State
{
    private bool PiggybankDestroyed;
    private bool ReturnToStart;
    public IdleRoseState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        PiggybankDestroyed = (brain as RoseBrain).PiggyDestroyed;
        if (PiggybankDestroyed)
        {
            (brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).Piggybank.transform.position);
            (brain as RoseBrain).MoveToState.SetIsMovingToPiggy(true);
            stateMachine.ChangeState((brain as RoseBrain).MoveToState);
        }
        else if(ReturnToStart)
        {
            (brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).StartPos);
            (brain as RoseBrain).MoveToState.SetIsMovingToPiggy(false);
            stateMachine.ChangeState((brain as RoseBrain).MoveToState);
        }
    }
    public void SetReturnToStart(bool isReturn)
    {
        ReturnToStart = isReturn;
    }
}
