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
            if((brain as RoseBrain).Piggybank.transform.position.x > (brain as RoseBrain).transform.position.x)
            {
                //Debug.Log("Idle move to pig RIGHT");
                (brain as RoseBrain).MoveToRightState.SetDestPoint((brain as RoseBrain).Piggybank.transform.position);
                //Debug.Log((brain as RoseBrain).Piggybank.transform.position);
                (brain as RoseBrain).MoveToRightState.SetIsMovingToPiggy(true);
                stateMachine.ChangeState((brain as RoseBrain).MoveToRightState);
            }else if((brain as RoseBrain).Piggybank.transform.position.x < (brain as RoseBrain).transform.position.x)
            {
                //Debug.Log("Idle move to pig LEFT");
                (brain as RoseBrain).MoveToLeftState.SetDestPoint((brain as RoseBrain).Piggybank.transform.position);
                //Debug.Log((brain as RoseBrain).Piggybank.transform.position);
                (brain as RoseBrain).MoveToLeftState.SetIsMovingToPiggy(true);
                stateMachine.ChangeState((brain as RoseBrain).MoveToLeftState);
            }


        }
        else if (ReturnToStart)
        {
            if ((brain as RoseBrain).rosePlatformPosition.transform.position.x > (brain as RoseBrain).transform.position.x)
            {
                //Debug.Log("Idle move to start, moving RIGHT");
                (brain as RoseBrain).MoveToRightState.SetDestPoint((brain as RoseBrain).rosePlatformPosition.transform.position);
                (brain as RoseBrain).MoveToRightState.SetIsMovingToPiggy(false);
                stateMachine.ChangeState((brain as RoseBrain).MoveToRightState);
            }
            else if ((brain as RoseBrain).rosePlatformPosition.transform.position.x < (brain as RoseBrain).transform.position.x)
            {
                //Debug.Log("Idle move to start, moving RIGHT");
                (brain as RoseBrain).MoveToLeftState.SetDestPoint((brain as RoseBrain).rosePlatformPosition.transform.position);
                (brain as RoseBrain).MoveToLeftState.SetIsMovingToPiggy(false);
                stateMachine.ChangeState((brain as RoseBrain).MoveToLeftState);
            }

            //Debug.Log("Idle move to start");
            //(brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).StartPos);
           /* (brain as RoseBrain).MoveToState.SetDestPoint((brain as RoseBrain).rosePlatformPosition.transform.position);
            (brain as RoseBrain).MoveToState.SetIsMovingToPiggy(false);
            stateMachine.ChangeState((brain as RoseBrain).MoveToState);*/
        }
    }
    public void SetReturnToStart(bool isReturn)
    {
        ReturnToStart = isReturn;
    }
}