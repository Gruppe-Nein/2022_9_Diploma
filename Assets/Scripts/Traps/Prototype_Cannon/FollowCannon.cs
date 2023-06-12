using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FollowCannon : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for communication between zone and cannon within this zone.")]
    [SerializeField] FollowCannonEventChannel _followEventChannel;
    [SerializeField] ChronoData _cData;
    #endregion

    [SerializeField] private float _rotationSpeed;
    private Transform _target;
    [SerializeField] private Cannonball _cannonball;
    [SerializeField] private Transform _shootingPoint;
    private bool _isActive;
    private bool _isStopped;
    private float _velocityController;

    [SerializeField] private float _cooldown;
    [SerializeField] private float _time;
    private ObjectPool<Cannonball> _pool;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Cannonball>(CreateCannonball, OnGetCannonball, OnReleaseCannonball, OnDestroyCannonball, false, 10, 20);
    }

    void Start()
    {
        _isActive = false;
        _isStopped = false;
        _velocityController = 1;
        _followEventChannel.OnEnterZoneFollow += Follow;
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (_isStopped && _velocityController > 0.1)
        {
            _velocityController *= _cData.velocityFactor;
        }
        else if (_isStopped && _velocityController < 0.1)
        {
            _velocityController = 0;
        }

        //Debug.Log("_velocityController: " + _velocityController);

        //Vector directed from the cannon to the targer object
        Vector3 direction = (_target.position - _shootingPoint.position).normalized;

        // Transformations necessary for correct rotation in 2D
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        // Rotate to the target rotation at the set speed until the expected rotation value is reached
        _shootingPoint.rotation = Quaternion.Slerp(_shootingPoint.rotation, q, Time.deltaTime * _rotationSpeed * _velocityController);
    }

    private void Follow(bool isFollowing, Transform target)
    {
        _isActive = isFollowing;
        _target = target;
        if (isFollowing)
            _shootCoroutine = StartCoroutine(Shoot());
        else
            StopCoroutine(_shootCoroutine);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (!_isStopped)
            {
                yield return new WaitForSeconds(_cooldown);
                _pool.Get();
                _isActive = false;
                yield return new WaitForSeconds(_time);
                _isActive = true;
            }
            yield return new WaitForSeconds(_time);
        }
    }

    private void OnDisable()
    {
        _followEventChannel.OnEnterZoneFollow -= Follow;
    }

    private void OnDestroy()
    {
        _followEventChannel.OnEnterZoneFollow -= Follow;
    }

    #region CANNON TIME ZONE BEHAVIOR
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
            _isStopped = false;
            _velocityController = 1;
        }
    }
    #endregion

    #region POOL METHODS
    private Cannonball CreateCannonball()
    {
        Cannonball ball = Instantiate(_cannonball, _shootingPoint.position, _shootingPoint.rotation);
        ball.SetPool(_pool);
        return ball;
    }

    private void OnGetCannonball(Cannonball ball)
    {
        ball.transform.position = _shootingPoint.position;
        ball.transform.rotation = _shootingPoint.rotation;
        ball.gameObject.SetActive(true);
    }

    private void OnReleaseCannonball(Cannonball ball)
    {
        ball.gameObject.SetActive(false);
    }

    private void OnDestroyCannonball(Cannonball ball)
    {
        Destroy(ball.gameObject);
    }
    #endregion
}