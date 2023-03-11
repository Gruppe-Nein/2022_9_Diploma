using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : State
{
    private float speed;

    public PatrollingState(ZombieBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = brain.MoveSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //Move(brain.MoveSpeed, brain.player);
    }
}
