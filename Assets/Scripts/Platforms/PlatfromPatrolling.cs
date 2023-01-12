using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        _rBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _platformSpeed = _speed;
        _targetPos = _posB.position;
        CalculateDirection();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _posA.position) < 0.05f)
        {
            _targetPos = _posB.position;
            CalculateDirection();
        }
        if (Vector2.Distance(transform.position, _posB.position) < 0.05f)
        {
            _targetPos = _posA.position;
            CalculateDirection();
        }
    }

    private void FixedUpdate()
    {
        _rBody2D.velocity = _moveDirection * _platformSpeed;
    }

    private void CalculateDirection()
    {
        _moveDirection = (_targetPos - transform.position).normalized;
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
}
