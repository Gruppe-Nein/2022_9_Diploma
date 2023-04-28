using UnityEngine;

public class RoseBrain : EnemyBrain
{
    public IdleRoseState IdleRoseState;
    public GrabPiggybankState GrabPiggybankState;

    #region Components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public RoseMovement movement;
    #endregion

    [HideInInspector] public bool PyggyDestroyed;
    [SerializeField] public GameObject PiggybankTop;
    [SerializeField] public GameObject PiggybankBot;
    [SerializeField] public GameObject PiggybankMid;

    [HideInInspector] public GameObject DestroyedPiggibank;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        stateMachine = new StateMachine();
        IdleRoseState = new IdleRoseState(this, stateMachine);
        GrabPiggybankState = new GrabPiggybankState(this, stateMachine);
        stateMachine.Initialize(IdleRoseState);
    }

    void Update()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();

        IsChasing = Vector2.Distance(rb.position, player.transform.position) < AggroRange;
    }
    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

}
