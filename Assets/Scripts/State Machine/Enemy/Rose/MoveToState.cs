using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToState : State
{
    private Vector3 DestPoint;
    private bool IsMovingToPiggy;
    public MoveToState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log("Move");
        //Debug.Log("MoveToPiggy" + IsMovingToPiggy);
        /*Debug.Log($"reached {(brain as RoseBrain).transform.position == DestPoint}");
        Debug.Log($"current {(brain as RoseBrain).transform.position}");
        Debug.Log($"dest {DestPoint}");*/
        //Debug.Log($"eaten {(brain as RoseBrain).CandyEaten}" );
        if (IsMovingToPiggy && (brain as RoseBrain).transform.position == DestPoint && (brain as RoseBrain).CandyEaten)
        {
            (brain as RoseBrain).PiggyDestroyed = false;
            (brain as RoseBrain).CandyEaten = false;
            (brain as RoseBrain).movement.reached = true;
            (brain as RoseBrain).IdleRoseState.SetReturnToStart(true);
            //Debug.Log("move return");
            stateMachine.ChangeState((brain as RoseBrain).IdleRoseState);
        }
        else if (!IsMovingToPiggy && (brain as RoseBrain).transform.position == DestPoint)
        {
            //Debug.Log("Idle return");
            (brain as RoseBrain).IdleRoseState.SetReturnToStart(false);
            stateMachine.ChangeState((brain as RoseBrain).IdleRoseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        (brain as RoseBrain).movement.MoveTo(brain.MoveSpeed, DestPoint, IsMovingToPiggy);
    }

    public void SetDestPoint(Vector3 point)
    {
        DestPoint = point;
    }
    public void SetIsMovingToPiggy(bool isPiggy)
    {
        IsMovingToPiggy = isPiggy;
    }

}