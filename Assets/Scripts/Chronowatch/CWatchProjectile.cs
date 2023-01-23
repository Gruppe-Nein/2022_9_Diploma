using System.Collections;
using UnityEngine;

public class CWatchProjectile : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D _rb;
    #endregion

    #region REFERENCES
    private GameObject _player;
    private GameObject _skipGround;
    [SerializeField] private ChronoZone _cZone;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region
    [SerializeField] private float _forceDeplayTimeOut;
    #endregion

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        _skipGround = GameObject.FindGameObjectWithTag("SkippableGround");        
        if (_skipGround != null)
        {
            Physics2D.IgnoreCollision(_skipGround.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * _cData.projectileSpeed;
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
        yield return new WaitForSeconds(_forceDeplayTimeOut);
        _cChannel.ChronoZoneDeploy(false);
        Destroy(gameObject);
    }
}