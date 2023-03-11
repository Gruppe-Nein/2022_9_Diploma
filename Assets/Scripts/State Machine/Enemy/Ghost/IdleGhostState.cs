using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleGhostState : State
{
    private bool playerInRange;

    public IdleGhostState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        playerInRange = brain.IsChasing;
        base.LogicUpdate();
        if (playerInRange)
        {
            stateMachine.ChangeState((brain as GhostBrain).ChasingState);
        }
    }
}
