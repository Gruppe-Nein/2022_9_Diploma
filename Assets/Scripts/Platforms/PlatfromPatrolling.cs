using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatfromPatrolling : MonoBehaviour
{
    #region PLATFORM END POINTS
    [SerializeField] private Transform _posA;
    [SerializeField] private Transform _posB;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region LOCAL PARAMETERS
    [SerializeField] private float _speed;
    private float _platformSpeed;
    private Vector3 _targetPos;
    private Rigidbody2D _rBody2D;
    private Vector3 _moveDirection;
    #endregion

    private float _endPointCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _platformSpeed = _speed;
        _targetPos = _posA.position;
        CalculateDirection();
    }

    private void Update()
    {
        CheckOverlapCircle();
    }

    private void FixedUpdate()
    {
        CheckOverlapCircle();
        _rBody2D.velocity = _moveDirection * _platformSpeed;
    }

    private void CalculateDirection()
    {
        _moveDirection = (_targetPos - transform.position).normalized;
    }

    private void CheckOverlapCircle()
    {
        if (Physics2D.OverlapCircle(_posA.position, _endPointCheckRadius, _groundLayer))
        {
            _targetPos = _posB.position;
            CalculateDirection();
            return;
        }
        if (Physics2D.OverlapCircle(_posB.position, _endPointCheckRadius, _groundLayer))
        {
            _targetPos = _posA.position;
            CalculateDirection();
            return;
        }
    }
    
    #region PLATFORM TIME ZONE BEHAVIOR

    private void StopPlatform(bool isActive)
    {
        if (isActive && transform.parent.name == "ChronoZone(Clone)")
        {
            _platformSpeed = 0;
        }
        else if (!isActive)
        {
            _platformSpeed = _speed;
        }
    }

    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += StopPlatform;
    }

    private void OnDisable()
    {
        _cChannel.OnChronoZoneActive -= StopPlatform;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_posA.position, _endPointCheckRadius);
        Gizmos.DrawWireSphere(_posB.position, _endPointCheckRadius);
    }

}
