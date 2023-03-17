using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    #region Components
    [HideInInspector] public GameObject player;
    #endregion

    public StateMachine stateMachine;

    #region Properties
    public float MoveSpeed;
    protected float _speed;
    [SerializeField] protected float AggroRange;
    [SerializeField] protected bool ShowAggroRange;
    #endregion

    #region StateParameters
    [HideInInspector] public bool IsChasing;
    #endregion

    #region Scriptable Objects
    [SerializeField] protected ChronoEventChannel _cChannel;
    #endregion
}
