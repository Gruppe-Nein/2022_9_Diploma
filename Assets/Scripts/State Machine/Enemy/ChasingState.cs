using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChasingState : State
{
    private float speed;

    public ChasingState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = brain.MoveSpeed;
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
