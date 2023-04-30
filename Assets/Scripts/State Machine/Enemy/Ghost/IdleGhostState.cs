public class IdleGhostState : State
{
    private bool playerInRange;

    public IdleGhostState(GhostBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerInRange = (brain as GhostBrain).IsChasing;
        if (playerInRange)
        {
            stateMachine.ChangeState((brain as GhostBrain).ChasingState);
        }
    }
}
