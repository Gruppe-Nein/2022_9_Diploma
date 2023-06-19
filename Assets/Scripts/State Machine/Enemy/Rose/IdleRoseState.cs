using UnityEngine;

public class IdleRoseState : State
{
    private bool PiggybankDestroyed;
    private bool ReturnToStart;
    public IdleRoseState(RoseBrain brain, StateMachine stateMachine) : base(brain, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log("Idle");
        PiggybankDestroyed = (brain as RoseBrain).PiggyDestroyed;
        if (PiggybankDestroyed)
        {
            //Debug.Log("Idle move to pig");
            (brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).Piggybank.transform.position);
            //Debug.Log((brain as RoseBrain).Piggybank.transform.position);
            (brain as RoseBrain).MoveToState.SetIsMovingToPiggy(true);
            stateMachine.ChangeState((brain as RoseBrain).MoveToState);
        }
        else if(ReturnToStart)
        {
            //Debug.Log("Idle move to start");
            //(brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).StartPos);
            (brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).rosePlatformPosition.transform.position);
            (brain as RoseBrain).MoveToState.SetIsMovingToPiggy(false);
            stateMachine.ChangeState((brain as RoseBrain).MoveToState);
        }
    }
    public void SetReturnToStart(bool isReturn)
    {
        ReturnToStart = isReturn;
    }
}
