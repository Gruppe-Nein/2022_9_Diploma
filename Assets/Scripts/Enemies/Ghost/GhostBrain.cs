using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrain : MonoBehaviour
{
    public StateMachine stateMachine;
    public IdleState IdleState;
    public ChasingState ChasingState;

    #region Components
    [HideInInspector] public GhostMovement movement;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    #endregion

    #region Properties
    public float MoveSpeed;
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
}
