using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : State
{
    //private float speed;
    private bool playerInRange;

    public ChasingState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //speed = brain.MoveSpeed;
    }

    public override void LogicUpdate()
    {
        playerInRange = brain.IsChasing;
        base.LogicUpdate();
        if (!playerInRange)
        {
            stateMachine.ChangeState((brain as GhostBrain).IdleGhostState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        (brain as GhostBrain).movement.Move(brain.MoveSpeed, brain.player);
    }
}
