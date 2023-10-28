using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeftState : MoveToState
{
    public MoveToLeftState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
        // You can add any specific initialization for MoveToLeftState here.
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
