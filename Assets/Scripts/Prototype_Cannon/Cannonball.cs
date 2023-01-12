using Unity.VisualScripting;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private ChronoEventChannel _cChannel;

    private Rigidbody2D _rb;
    private Vector3 _cannonballVector;
    private bool _canDamage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cannonballVector = transform.right * _speed;
        _rb.velocity = _cannonballVector;
        _canDamage = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 5);
    }

    #region CANNONBALL TIME ZONE BEHAVIOR

    private void FreezeProjectile(bool isActive)
    {
        if (isActive && transform.parent != null)
        {
            if (transform.parent.name == "ChronoZone(Clone)") {
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                _canDamage = false;
            }
        }
        else if ((_rb.constraints & RigidbodyConstraints2D.FreezePositionX) == RigidbodyConstraints2D.FreezePositionX)
        {            
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.velocity = _cannonballVector;
            _canDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && _canDamage)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }        
    }
    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += FreezeProjectile;
    }

    private void OnDestroy()
    {
        _cChannel.OnChronoZoneActive -= FreezeProjectile;
    }
    #endregion
}