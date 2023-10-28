using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRightState : MoveToState
{
    public MoveToRightState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
        // You can add any specific initialization for MoveToRightState here.
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
