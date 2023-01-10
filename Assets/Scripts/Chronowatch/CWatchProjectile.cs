using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CWatchProjectile : MonoBehaviour
{
    [SerializeField] private ChronoData _cData;
    [SerializeField] private ChronoZone _cZone;
    private GameObject _player;
    private Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 cannonballVector = transform.right * _cData.projectileSpeed;
        _rb.velocity = cannonballVector;
        StartCoroutine(ForceDeploy());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            DeployChronoZone();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DeployChronoZone();
    }

    private void DeployChronoZone()
    {
        Instantiate(_cZone, transform.position, transform.rotation);        
        Destroy(gameObject);
    }

    private IEnumerator ForceDeploy()
    {
        yield return new WaitForSeconds(_cData.chronoZoneActiveTime);
        _cData.ChronoZoneDeploy(false);
        Destroy(gameObject);
    }
}
