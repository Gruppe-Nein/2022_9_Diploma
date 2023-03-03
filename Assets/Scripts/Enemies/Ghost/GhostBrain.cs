using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrain : MonoBehaviour
{
    public StateMachine stateMachine;
    public IdleState IdleState;
    public ChasingState ChasingState;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region Components
    [HideInInspector] public GhostMovement movement;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    #endregion

    #region Properties
    public float MoveSpeed;
    private float _speed;
    public float AggroRange;
    #endregion

    #region StateParameters
    public bool IsChasing;
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
        Debug.Log(stateMachine.CurrentState);
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
}
