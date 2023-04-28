using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPiggybankState : State
{
    public GrabPiggybankState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        (brain as RoseBrain).movement.MoveToPiggybank(brain.MoveSpeed);
    }
}
