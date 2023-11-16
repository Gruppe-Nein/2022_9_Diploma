using UnityEngine;

public class MoveOnEnterPlatformMorePoints : MonoBehaviour
{
    #region PLATFORM END POINTS
    [SerializeField] private Transform[] _positions;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    #region LOCAL PARAMETERS
    [SerializeField] private float _speed;
    private bool _isStopped;
    private float _platformSpeed;
    private Vector3 _targetPos;
    private Rigidbody2D _rBody2D;
    private Vector3 _moveDirection;
    private bool _isActivated;
    private GameObject _childGameObject;
    private int _currentIndex;
    private bool toAdd;
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
        _isStopped = false;
        _platformSpeed = _speed;
        _targetPos = _positions[0].position;
        _currentIndex = 0;
        toAdd = true;
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
        if (_isStopped && _platformSpeed > 0.1)
        {
            _platformSpeed *= _cData.velocityFactor;
        }
        else if (_isStopped && _platformSpeed < 0.1)
        {
            _platformSpeed = 0;
        }
        _rBody2D.velocity = _moveDirection * _platformSpeed;
    }

    public void setSpeed(float speed)
    {
        _speed = speed;
        _platformSpeed = speed;
    }

    private void CalculateDirection()
    {
        _moveDirection = (_targetPos - transform.position).normalized;
    }

    private void CheckChildActivationStatus()
    {
        if (_childGameObject.GetComponent<ObjectCheck>()._checkObjectIsOn)
        {
            _isActivated = true;
        }

    }
    private void CheckOverlapCircle()
    {
        if (Physics2D.OverlapCircle(_positions[_currentIndex].position, _endPointCheckRadius, _groundLayer))
        {
            if (toAdd)
            {
                _targetPos = _positions[++_currentIndex].position;
            }
            else
            {
                _targetPos = _positions[--_currentIndex].position;
            }
            CalculateDirection();
        }

        if (_currentIndex == _positions.Length - 1)
        {
            toAdd = false;
        }
        else if (_currentIndex == 0)
        {
            toAdd = true;
        }
    }

    #region PLATFORM TIME ZONE BEHAVIOR
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _platformSpeed = _speed;
            _isStopped = false;
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < _positions.Length; i++)
        {
            Gizmos.DrawWireSphere(_positions[i].position, _endPointCheckRadius);
        }
    }
}
