using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : State
{
    //private float speed;
    private bool playerInRange;
    private bool playerInAttackRange;

    public ChasingState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerInRange = (brain as GhostBrain).IsChasing;
        playerInAttackRange = (brain as GhostBrain).IsAttacking;
        
        if (!playerInRange )
        {
            stateMachine.ChangeState((brain as GhostBrain).IdleGhostState);
        }
        else if((brain as GhostBrain).isDisabled)
        {
            stateMachine.ChangeState((brain as GhostBrain).IdleGhostState);
            brain.gameObject.SetActive(false);
        }
        if(playerInAttackRange)
        {
            stateMachine.ChangeState((brain as GhostBrain).AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        (brain as GhostBrain).movement.Move(brain.MoveSpeed, brain.player);
    }
}
