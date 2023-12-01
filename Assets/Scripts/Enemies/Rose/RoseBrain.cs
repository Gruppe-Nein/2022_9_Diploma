using System;
using UnityEngine;

public class RoseBrain : EnemyBrain
{
    public IdleRoseState IdleRoseState;
    public MoveToState MoveToState;
    public MoveToLeftState MoveToLeftState;
    public MoveToRightState MoveToRightState;

    public GameObject rosePlatformPosition;

    #region Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public RoseMovement movement;
    #endregion

    [HideInInspector] public bool PiggyDestroyed;
    [HideInInspector] public bool CandyEaten;
    [HideInInspector] public GameObject Piggybank;
    //[SerializeField] private GameObject platform;
    //[HideInInspector] public Vector3 StartPos;


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
        MoveToLeftState = new MoveToLeftState(this, stateMachine);
        MoveToRightState = new MoveToRightState(this, stateMachine); 
        stateMachine.Initialize(IdleRoseState);

        GameEventSystem.Instance.OnPiggybankDestroy += PiggybankDestroyed;

        //StartPos = transform.position;
    }

    public String checkState()
    {
        return stateMachine.CurrentState.GetType().Name;
    }

    private void PiggybankDestroyed(GameObject obj)
    {
        PiggyDestroyed = true;
        Piggybank = obj;
    }

    public void StartPositionReturn()
    {
        GameEventSystem.Instance.RoseReturnToStart();
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
