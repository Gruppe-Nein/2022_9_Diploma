using Unity.VisualScripting;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private ChronoData _cData;
    [SerializeField] private CircleCollider2D _circleCollider;

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

    private void OnEnable()
    {
        _cData.OnChronoZoneActive += FreezeProjectile;
    }

    private void OnDestroy()
    {
        _cData.OnChronoZoneActive -= FreezeProjectile;
    }

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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && _canDamage)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }        
    }
}