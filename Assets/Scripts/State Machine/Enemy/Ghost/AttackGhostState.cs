using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackGhostState : State
{
    private bool playerInAttackRange;
    private bool reachedDestPoint;
    private Vector2 DestPosition;

    public AttackGhostState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DestPosition = GetDestPoint();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerInAttackRange = (brain as GhostBrain).IsAttacking;
        reachedDestPoint = (brain as GhostBrain).transform.position == new Vector3(DestPosition.x, DestPosition.y);
        
        if (!playerInAttackRange && reachedDestPoint)
        {
            stateMachine.ChangeState((brain as GhostBrain).ChasingState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        (brain as GhostBrain).movement.AttackCharge(DestPosition);
    }

    private Vector3 GetDestPoint()
    {
        Vector3 direction = (brain.player.transform.position - brain.transform.position).normalized;
        return brain.transform.position + direction * 25f;
    }
}
