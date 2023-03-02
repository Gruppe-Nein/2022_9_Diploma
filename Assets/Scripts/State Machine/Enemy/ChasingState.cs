using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : State
{
    public ChasingState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        playerInRange = brain.IsChasing;
        base.LogicUpdate();
        if (!playerInRange)
        {
            stateMachine.ChangeState(brain.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        brain.movement.Move(brain.MoveSpeed);
    }
}
