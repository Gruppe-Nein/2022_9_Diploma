using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
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
        _rigidbody.velocity = Vector2.up * -1f * _speed;
    }

    #region CANNONBALL TIME ZONE BEHAVIOR
    private void FreezeRock(bool isActive)
    {
        if (isActive && transform.parent != null)
        {
            if (transform.parent.name == "ChronoZone(Clone)")
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else if (_rigidbody.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;

        }
    }

    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += FreezeRock;
    }

    private void OnDestroy()
    {
        _cChannel.OnChronoZoneActive -= FreezeRock;
    }
    #endregion

    #region PORTAL METHODS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            transform.position = collision.GetComponent<Teleportal>().getAnotherPortal().position;
        }
    }
    #endregion
}
