using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class State
{
    protected GhostBrain brain;
    protected StateMachine stateMachine;
    protected bool playerInRange;

    #region SCRIPTABLE OBJECTS
    [SerializeField] protected ChronoEventChannel _cChannel;
    #endregion

    protected State(GhostBrain brain, StateMachine stateMachine)
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
