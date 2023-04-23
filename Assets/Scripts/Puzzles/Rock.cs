using UnityEngine;

public class Rock : MonoBehaviour, ITeleportable
{
    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region COMPONENTS
    private Rigidbody2D _rigidbody;
    #endregion

    #region PARAMETERS
    [SerializeField] private float _speed;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
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
            FreezeRock(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            FreezeRock(false);
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
