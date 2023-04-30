using System;
using UnityEngine;

public class RoseBrain : EnemyBrain
{
    public IdleRoseState IdleRoseState;
    public MoveToState MoveToState;

    #region Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public RoseMovement movement;
    #endregion

    [HideInInspector] public bool PiggyDestroyed;
    [HideInInspector] public bool CandyEaten;
    [HideInInspector] public GameObject Piggybank;
    [HideInInspector] public Vector3 StartPos;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = GetComponent<RoseMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        stateMachine = new StateMachine();
        IdleRoseState = new IdleRoseState(this, stateMachine);
        MoveToState = new MoveToState(this, stateMachine);
        stateMachine.Initialize(IdleRoseState);

        GameEventSystem.Instance.OnPiggybankDestroy += PiggybankDestroyed;

        StartPos = transform.position;
    }

    private void PiggybankDestroyed(GameObject obj)
    {
        PiggyDestroyed = true;
        Piggybank = obj;
    }

    void Update()
    {        
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
    }
    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

}
