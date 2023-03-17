using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBrain : EnemyBrain
{
    private IdlePartollingState _idlePartollingState;
    private void Awake()
    {
        _speed = MoveSpeed;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        _idlePartollingState = new IdlePartollingState(this, stateMachine);
        

        stateMachine.Initialize(_idlePartollingState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
