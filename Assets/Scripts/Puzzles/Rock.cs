using UnityEngine;
using UnityEngine.EventSystems;

public class Rock : MonoBehaviour, ITeleportable
{
    #region SCRIPTABLE OBJECTS
    //[SerializeField] private ChronoEventChannel _cChannel;
    [SerializeField] private ChronoData _cData;
    #endregion

    #region COMPONENTS
    private Rigidbody2D _rigidbody;
    #endregion

    #region PARAMETERS
    [SerializeField] private float _speed;
    private float  _maxSpeed;
    private bool _isStopped;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _isStopped = false;
        _maxSpeed = _speed;
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
        _rigidbody.velocity = Vector2.down * _speed;
    }

    #region ROCK TIME ZONE BEHAVIOR
    private void FreezeRock(bool isActive)
    {
        if (isActive)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            //FreezeRock(true);
            _isStopped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = false;
            _speed = _maxSpeed;
            //FreezeRock(false);
        }
    }
    #endregion

    /// <summary>
    /// ITeleportable interface method implementation.
    /// </summary>
    /// <param name="position">Destination for teleport</param>
    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }
}
