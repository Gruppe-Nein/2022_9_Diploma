using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveOnEnterPlatform : MonoBehaviour
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
    private bool _isActivated;
    private GameObject _childGameObject;
    #endregion

    private float _endPointCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rBody2D = GetComponent<Rigidbody2D>();
        _childGameObject = transform.GetChild(0).gameObject;
    }

    private void Start()
    {

        _isActivated = false;

        _platformSpeed = _speed;
        _targetPos = _posA.position;
        CalculateDirection();
    }    

    private void FixedUpdate()
    {
        CheckChildActivationStatus();
        if (!_isActivated)
        {
            return;
        }
        CheckOverlapCircle();
        _rBody2D.velocity = _moveDirection * _platformSpeed;
        
    }

    private void CalculateDirection()
    {
        _moveDirection = (_targetPos - transform.position).normalized;
    }

    private void CheckChildActivationStatus()
    {
        if(_childGameObject.GetComponent<ObjectCheck>()._checkObjectIsOn)
        {
            _isActivated = true;
        }
     
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
        if (isActive)
        {
            _platformSpeed = 0;
        }
        else
        {
            _platformSpeed = _speed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopPlatform(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopPlatform(false);
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_posA.position, _endPointCheckRadius);
        Gizmos.DrawWireSphere(_posB.position, _endPointCheckRadius);
    }

   

}
