public abstract class State
{
    protected EnemyBrain brain;
    protected StateMachine stateMachine;

    protected State(EnemyBrain brain, StateMachine stateMachine)
    {
        this.brain = brain;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
       
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
