using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoZone : MonoBehaviour
{
    [SerializeField] private ChronoData _cData;

    private CircleCollider2D _circleCollider;
    private float _radius;

    private Dictionary<string, Transform> _parents;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _radius = _circleCollider.radius;

        _parents = new Dictionary<string, Transform>();
    }

    void Start()
    {
        StartCoroutine(ActiveTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") || collision.CompareTag("Projectile"))
        {
            _parents.TryAdd(collision.gameObject.name, collision.transform.parent);
            collision.transform.SetParent(this.transform);
            _cData.ChronoZoneActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") || collision.CompareTag("Projectile"))
        {
            collision.transform.SetParent(_parents[collision.gameObject.name]);
            _cData.ChronoZoneActive(false);
        }
    }

    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(_cData.chronoZoneActiveTime);
        while (_circleCollider.radius != 0.01f)
        {
            _radius -= 2.0f;
            if (_radius < 0)
            {
                _radius = 0.01f;
            }
            yield return new WaitForSeconds(0.5f);
            _circleCollider.radius = _radius;            
        }
        yield return new WaitForSeconds(0.5f);
        _cData.ChronoZoneDeploy(false);
        Destroy(gameObject);
    }
}
