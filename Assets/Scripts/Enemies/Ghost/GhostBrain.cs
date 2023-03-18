using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrain : MonoBehaviour
{
    #region Components
    public StateMachine stateMachine;
    public IdleState IdleState;
    public ChasingState ChasingState;
    [HideInInspector] public GhostMovement movement;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    #endregion

    #region Scriptable Objects
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region Properties
    public float MoveSpeed;
    private float _speed;
    public float AggroRange;
    public bool ShowAggroRange;
    #endregion

    #region StateParameters
    [HideInInspector] public bool IsChasing;
    #endregion

    private void Awake()
    {
        movement = GetComponent<GhostMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        _speed = MoveSpeed;
    }

    void Start()
    {
        stateMachine = new StateMachine();
        IdleState = new IdleState(this, stateMachine);
        ChasingState = new ChasingState(this, stateMachine);

        stateMachine.Initialize(IdleState);
    }

    void Update()
    {
        //Debug.Log(stateMachine.CurrentState);
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();

        IsChasing = Vector2.Distance(rb.position, player.transform.position) < AggroRange;
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #region GHOST TIME ZONE BEHAVIOR
    private void StopGhost(bool isActive)
    {
        if (isActive && transform.parent.name == "ChronoZone(Clone)")
        {
            MoveSpeed = 0;
        }
        else if (!isActive)
        {
            MoveSpeed = _speed;
        }
    }

    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += StopGhost;
    }

    private void OnDisable()
    {
        _cChannel.OnChronoZoneActive -= StopGhost;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if(ShowAggroRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }
    }
}
