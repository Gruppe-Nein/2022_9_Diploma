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
    public float ChargeSpeed;
    protected float _speed;
    [SerializeField] protected float AggroRange;
    [SerializeField] protected bool ShowAggroRange;
    #endregion

    

    #region Scriptable Objects
    [SerializeField] protected ChronoEventChannel _cChannel;
    #endregion
}
