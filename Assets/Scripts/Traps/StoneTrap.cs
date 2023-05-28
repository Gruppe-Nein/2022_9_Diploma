using UnityEngine;

public class StoneTrap : MonoBehaviour
{
    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    #region LOCAL PARAMETERS
    [SerializeField] private bool _spiked;
    [SerializeField] private bool _horizontal;
    [SerializeField] private float _speed;
    private float _stoneSpeed;
    [SerializeField] private float _speedMod;
    [SerializeField] private Transform[] _positions;
    private Transform _targetWaypoint;
    private int _currentPoint = 0;
    private int _currentDirModifier = 1;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private bool _canFollow;
    private Vector3 _playerDir;

    private bool _isStopped;

    private Animator _animator;
    #endregion

    void Start()
    {
        _animator = transform.GetComponent<Animator>();
        _targetWaypoint = _positions[0];
        _isStopped = false;
        _stoneSpeed = _speed;
        _canFollow = false;
    }

    public void setFollow(bool canFollow, Vector3 playerDir)
    {
        _canFollow = canFollow;
        if (_horizontal)
        {
            _playerDir = playerDir * Vector2.right;
        }
        else
        {
            _playerDir = playerDir * Vector2.up;
        }
        
    }

    void Update()
    {
        if (_isStopped && _stoneSpeed > 0.1)
        {
            _stoneSpeed *= _cData.velocityFactor;
            _animator.speed *= _cData.velocityFactor;
        }
        else if (_isStopped && _stoneSpeed < 0.1)
        {
            _stoneSpeed = 0;
            _animator.speed = 0;
        }

        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else if (!_waiting)
        {
            if (!_canFollow)
            {
                transform.position = Vector2.MoveTowards(transform.position, _targetWaypoint.position, _stoneSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, _targetWaypoint.position) <= 0.02f)
                {
                    ResetWaiting();
                    _targetWaypoint = GetNextPoint();
                }
            }
            else
            {
                transform.Translate(_playerDir * _stoneSpeed * _speedMod * Time.deltaTime);
            }
        }

    }

    private Transform GetNextPoint()
    {        
        if (_currentPoint == _positions.Length-1)
        {
            _currentDirModifier = -1; 
        }
        else if (_currentPoint == 0)
        {
            _currentDirModifier = 1;
        }

        _currentPoint += _currentDirModifier;

        return _positions[_currentPoint];
    }

    private void ResetAnimation()
    {
        _animator.SetBool("rightHit", false);
        _animator.SetBool("leftHit", false);
        _animator.SetBool("VertHit", false);
    }

    private void ResetWaiting()
    {
        _waitCounter = 0f;
        _waiting = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_playerDir.x > 0)
            {
                _animator.SetBool("rightHit", true);
            }
            else if (_playerDir.x < 0)
            {
                _animator.SetBool("leftHit", true);
            }

            if (_playerDir.y > 0)
            {
                _animator.SetBool("VertHit", true);
                _animator.SetBool("rightHit", true);
            } else if (_playerDir.y < 0)
            {
                _animator.SetBool("VertHit", true);
                _animator.SetBool("leftHit", true);
            }

            ResetWaiting();

            _canFollow = false;
        }
        if (_spiked && collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            _stoneSpeed = _speed;
            _animator.speed = 1f;
            _isStopped = false;
        }
    }
}