using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrain : EnemyBrain
{
    
    public IdleGhostState IdleGhostState;
    public ChasingState ChasingState;
    public AttackGhostState AttackState;

    #region Components
    //[HideInInspector] public GameObject player;
    /*public StateMachine stateMachine;
    public IdleState IdleState;
    public ChasingState ChasingState;*/
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public GhostMovement movement;
    #endregion

    #region StateParameters
    [HideInInspector] public bool IsChasing;
    [HideInInspector] public bool IsAttacking;
    [HideInInspector] public bool isDisabled;
    [SerializeField] public float AttackRange;
    
    #endregion

    /*#region Properties
    public float MoveSpeed;
    private float _speed;
    public float AggroRange;
    public bool ShowAggroRange;
    #endregion*/

    /*#region StateParameters
    [HideInInspector] public bool IsChasing;

    #endregion*/

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = GetComponent<GhostMovement>();
        rb = GetComponent<Rigidbody2D>();
        _speed = MoveSpeed;
    }

    void Start()
    {
        stateMachine = new StateMachine();
        IdleGhostState = new IdleGhostState(this, stateMachine);
        ChasingState = new ChasingState(this, stateMachine);
        AttackState = new AttackGhostState(this, stateMachine);

        /*stateMachine.states.Add(IdleGhostState);
        stateMachine.states.Add(ChasingState);*/

        stateMachine.Initialize(IdleGhostState);
        GameEventSystem.Instance.OnPlayerInSafeZone += SetDisabled;
    }

    void Update()
    {
        //Debug.Log(stateMachine.CurrentState);
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();

        IsAttacking = Vector2.Distance(rb.position, player.transform.position) < AttackRange;
        IsChasing = Vector2.Distance(rb.position, player.transform.position) < AggroRange && !IsAttacking;
        
        
    }

    private void SetDisabled(bool active)
    {
        isDisabled = active;
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #region GHOST TIME ZONE BEHAVIOR
    private void StopGhost(bool isActive)
    {
        if (isActive)
        {
            MoveSpeed = 0;
        }
        else
        {
            MoveSpeed = _speed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            
            StopGhost(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopGhost(false);
        }
    }
    /*
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
    */

    #endregion

    private void OnDrawGizmos()
    {
        if (ShowAggroRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, AggroRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}
