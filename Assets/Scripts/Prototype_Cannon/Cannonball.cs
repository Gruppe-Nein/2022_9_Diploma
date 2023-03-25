using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Cannonball : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D _rb;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region PARAMETERS
    [SerializeField] private int _speed;
    private Vector3 _cannonballVector;
    private bool _canDamage;
    private IObjectPool<Cannonball> _pool;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cannonballVector = transform.right * _speed;
        _rb.velocity = _cannonballVector;
        _canDamage = true;
    }

    #region CANNONBALL TIME ZONE BEHAVIOR

    private void FreezeProjectile(bool isActive)
    {
        if (isActive)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _canDamage = false;
        }
        else
        {            
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.velocity = _cannonballVector;
            _canDamage = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            FreezeProjectile(true);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            _pool.Release(this);
        }

        if (collision.gameObject.CompareTag("Player") && _canDamage)
        {
            GameEventSystem.Instance.PlayerTakeDamage(1);
            _pool.Release(this);
        }

        ContinueMovement(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContinueMovement(collision);
    }

    #region POOL METHODS
    private void OnBecameVisible()
    {
        _rb.velocity = _cannonballVector;
    }

    public void SetPool(IObjectPool<Cannonball> pool)
    {
        _pool = pool;
    }

    private void ContinueMovement(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            _rb.velocity = _cannonballVector;
        }
    }
    #endregion
}