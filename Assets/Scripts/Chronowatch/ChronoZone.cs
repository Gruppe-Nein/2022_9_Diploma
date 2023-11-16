using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ChronoZone : MonoBehaviour
{
    #region COMPONENTS
    private CircleCollider2D _circleCollider;
    [SerializeField] private SpriteRenderer _openedWatch;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region LOCAL VARIABLES
    private float _radius;
    private Animator _spriteAnimator;
    private Animator _spriteAnimator1;
    //private Dictionary<string, Transform> _parents;
    #endregion

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _radius = _circleCollider.radius;
        _spriteAnimator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        _spriteAnimator1 = this.transform.GetChild(1).gameObject.GetComponent<Animator>();
        //_parents = new Dictionary<string, Transform>();
        _cChannel.onCheckPointRestore += RestoreWatch;
    }

    void Start()
    {
        StartCoroutine(ActiveTime());

        if (transform.localRotation.z > 0.5f || transform.localRotation.z < -0.5f)
        {
            _openedWatch.flipX = true;
            _openedWatch.flipY = true;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") || collision.CompareTag("Platform") 
            || collision.CompareTag("CircleMaze") || collision.CompareTag("Fallable")
            || collision.CompareTag("Ghost"))
        {
            _parents.TryAdd(collision.gameObject.name, collision.transform.parent);
            collision.transform.SetParent(this.transform);
            _cChannel.ChronoZoneActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") || collision.CompareTag("Platform") 
            || collision.CompareTag("CircleMaze") || collision.CompareTag("Fallable")
            || collision.CompareTag("Ghost"))
        {
            collision.transform.SetParent(_parents[collision.gameObject.name]);
            _cChannel.ChronoZoneActive(false);
        }
    }*/

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
        _cChannel.ChronoZoneDeploy(false);
        _spriteAnimator.ResetTrigger("IsDeployed");
        _spriteAnimator1.ResetTrigger("IsDeployed");
        Destroy(gameObject);
    }

    private void RestoreWatch(bool reset)
    {
        if (reset)
        {
            _cChannel.ChronoZoneDeploy(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _cChannel.onCheckPointRestore -= RestoreWatch;
    }
}