using UnityEngine;
using UnityEngine.Pool;

public class Cannon : MonoBehaviour
{
    #region REFERENCES
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Cannonball _cannonball;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region PARAMETERS
    [SerializeField] private float _cooldown;
    private float _nextTime;
    private bool _isShooting;
    private ObjectPool<Cannonball> _pool;
    #endregion

    private void Awake()
    {
        _pool = new ObjectPool<Cannonball>(CreateCannonball, OnGetCannonball, OnReleaseCannonball, OnDestroyCannonball, false, 10, 20);
    }

    private void Start()
    {
        _nextTime = Time.time + _cooldown;
        _isShooting = true;        
    }

    private void Update()
    {
        if (_isShooting && Time.time > _nextTime)
        {
            _pool.Get();
            _nextTime = Time.time + _cooldown;
        }
    }

    #region CANNON TIME ZONE BEHAVIOR

    private void StopShoot(bool isActive)
    {
        if (isActive && transform.parent != null)
        {
            if (transform.parent.name == "ChronoZone(Clone)")
            {
                _isShooting = false;
            }            
        } 
        else
        {
            _isShooting = true;
        }        
    }
    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += StopShoot;
    }

    private void OnDisable()
    {
        _cChannel.OnChronoZoneActive -= StopShoot;
    }
    #endregion

    #region POOL METHODS
    private Cannonball CreateCannonball()
    {
        Cannonball ball = Instantiate(_cannonball, _firePoint.position, _firePoint.rotation);
        ball.SetPool(_pool);
        return ball;
    }

    private void OnGetCannonball(Cannonball ball)
    {        
        ball.transform.position = _firePoint.position;
        ball.transform.rotation = _firePoint.rotation;
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
