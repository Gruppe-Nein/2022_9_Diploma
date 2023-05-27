using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Cannonball : MonoBehaviour, ITeleportable
{
    #region COMPONENTS
    private Rigidbody2D _rb;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    #region PARAMETERS
    [SerializeField] private float _speed;
    private float _maxSpeed;
    private bool _isStopped;
    private bool _canDamage;
    private IObjectPool<Cannonball> _pool;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _maxSpeed = _speed;
        _rb.velocity = transform.right * _speed;
        _canDamage = true;
    }

    private void FixedUpdate()
    {
        if (_isStopped && _speed > 0.1)
        {
            _speed *= _cData.velocityFactor;
        }
        else if (_isStopped && _speed < 0.1)
        {
            _speed = 0;
        }
        _rb.velocity = transform.right * _speed;
    }

    #region CANNONBALL TIME ZONE BEHAVIOR
    private void FreezeProjectile(bool isActive)
    {
        if (isActive)
        {
            _isStopped = true;
            _canDamage = false;
        }
        else
        {                       
            _isStopped = false;
            _canDamage = true;
            _speed = _maxSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            FreezeProjectile(false);
        }
    }
    /*
    private void FreezeProjectile(bool isActive)
    {
        if (isActive && transform.parent != null)
        {
            if (transform.parent.name == "ChronoZone(Clone)") {
                _rigibBody.constraints = RigidbodyConstraints2D.FreezeAll;
                _canDamage = false;
            }
        }
        else if ((_rigibBody.constraints & RigidbodyConstraints2D.FreezePositionX) == RigidbodyConstraints2D.FreezePositionX)
        {            
            _rigibBody.constraints = RigidbodyConstraints2D.None;
            _rigibBody.velocity = _cannonballVector;
            _canDamage = true;
        }
    }
    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += FreezeProjectile;
    }

    private void OnDestroy()
    {
        _cChannel.OnChronoZoneActive -= FreezeProjectile;
    }*/
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            FreezeProjectile(true);
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ballbarrier"))
        {
            _pool.Release(this);
        }

        if (collision.gameObject.CompareTag("Player") && _canDamage)
        {
            GameEventSystem.Instance.PlayerTakeDamage(1);
            _pool.Release(this);
        }

        if (collision.gameObject.CompareTag("Destructable"))
        {
            Destroy(collision.gameObject);
            _pool.Release(this);
        }

        //ContinueMovement(collision);
    }

    #region POOL METHODS
    private void OnBecameVisible()
    {
        _speed = _maxSpeed;
    }

    public void SetPool(IObjectPool<Cannonball> pool)
    {
        _pool = pool;
    }

    //private void ContinueMovement(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Projectile"))
    //    {
    //        _speed = _maxSpeed;
    //    }
    //}
    #endregion

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }
}