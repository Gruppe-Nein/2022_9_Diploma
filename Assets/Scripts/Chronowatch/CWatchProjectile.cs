using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CWatchProjectile : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D _rigibBody;
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region REFERENCES
    private GameObject _player;
    private GameObject _skipGround;
    [SerializeField] private ChronoZone _cZone;
    [SerializeField] private CircleCollider2D _cCollider;
    [SerializeField] private Sprite[] _sprites;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    [SerializeField] private ChronoEventChannel _cChannel;
    [SerializeField] private InputEventChannel _iEventChannel;
    #endregion

    #region PARAMETERS
    private bool _returnToPlayer;
    private bool _onCooldown;
    #endregion

    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _cCollider);

        _skipGround = GameObject.FindGameObjectWithTag("SkippableGround");        
        if (_skipGround != null)
        {
            Physics2D.IgnoreCollision(_skipGround.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (transform.localRotation.z > 0.5f || transform.localRotation.z < -0.5f)
        {
            _spriteRenderer.flipX = true;
            _spriteRenderer.flipY = true;
        }

        _rigibBody = GetComponent<Rigidbody2D>();
        _rigibBody.freezeRotation = true;
        _rigibBody.velocity = transform.right * _cData.projectileSpeed;

        _returnToPlayer = false;
        _onCooldown = false;

        StartCoroutine(ActivationTimeOut());
    }

    public void Update()
    {
        //Returns chronowatch back to its original position
        if (_returnToPlayer)
        {
            _rigibBody.velocity = ((_player.transform.position + _cData.offsetPosition) - transform.position) * _cData.projectileReturnSpeed;
            
            float distance = Vector3.Distance(transform.position, _player.transform.position + _cData.offsetPosition);

            if (distance < _cData.thresholdDistance)
            {
                _returnToPlayer = false;
                Destroy(gameObject);
                _cChannel.WatchProjectileDeploy(false);
            }
        }
    }

    public void ForceDeployChronoZone(InputAction.CallbackContext context)
    {
        if (context.performed && !_onCooldown)
        {
            _onCooldown = true;
            DeployChronoZone();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DeployChronoZone();
    }

    private void DeployChronoZone()
    {
        _rigibBody.velocity = Vector2.zero;
        _cCollider.enabled = false;
        _spriteRenderer.sprite = _sprites[1];
        Instantiate(_cZone, transform.position, transform.rotation); 
    }

    private IEnumerator ActivationTimeOut()
    {
        yield return new WaitForSeconds(_cData.activTimeOut);
        if (!_rigibBody.velocity.Equals(Vector2.zero))
        {
            _cChannel.ChronoZoneDeploy(false);
        }
    }

    private void DestroyWatch(bool isDeployed)
    {
        _spriteRenderer.sprite = _sprites[0];
        _returnToPlayer = true;
    }

    private void RestoreWatch(bool reset)
    {
        if (reset)
        {
            _cChannel.WatchProjectileDeploy(false);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _cChannel.onChronoZoneDeploy += DestroyWatch;
        _cChannel.onCheckPointRestore += RestoreWatch;
        _iEventChannel.onShootButtonPressed += ForceDeployChronoZone;
    }

    private void OnDestroy()
    {
        _cChannel.onChronoZoneDeploy -= DestroyWatch;
        _cChannel.onCheckPointRestore -= RestoreWatch;
        _iEventChannel.onShootButtonPressed -= ForceDeployChronoZone;
    }
}