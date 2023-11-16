using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CWatchProjectile : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D _rigibBody;
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region REFERENCES
    private GameObject _player;
    private GameObject _skipMazeWalls;
    private GameObject _skipMazeGround;
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
    private Vector2 _deployPosition;
    #endregion

    public static Action<CWatchProjectile> Instantiator(Vector2 deployPosition)
    {
        return (self) =>
        {
            self._deployPosition = deployPosition;
        };
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _cCollider);

        _skipMazeGround = GameObject.FindGameObjectWithTag("SkippableGround");
        _skipMazeWalls = GameObject.FindGameObjectWithTag("SkippableWalls");

        disableCollisition(_skipMazeGround);
        disableCollisition(_skipMazeWalls);
        
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (transform.localRotation.z > 0.5f || transform.localRotation.z < -0.5f)
        {
            _spriteRenderer.flipX = true;
            _spriteRenderer.flipY = true;
        }

        _rigibBody = GetComponent<Rigidbody2D>();
        _rigibBody.freezeRotation = true;
        
        //_rigibBody.velocity = transform.right * _cData.projectileSpeed;

        _returnToPlayer = false;
        _onCooldown = false;

        StartCoroutine(ActivationTimeOut());
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _deployPosition, _cData.projectileSpeed * Time.deltaTime);

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

        if (Vector2.Distance(transform.position, _deployPosition) <= 0.1f && !_onCooldown)
        {
            _onCooldown = true;
            DeployChronoZone();
        }
    }

    public void ReturnWatchBeforeDeployingChronoZone(InputAction.CallbackContext context)
    {
        if (context.performed && !_onCooldown)
        {
            _onCooldown = true;
            _cChannel.ChronoZoneDeploy(false);
            _cChannel.WatchProjectileDeploy(false);
        }        
    }

    private void disableCollisition(GameObject skipObject)
    {
        if (skipObject != null)
        {
            Physics2D.IgnoreCollision(skipObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _onCooldown = true;
        DeployChronoZone();
    }

    private void DeployChronoZone()
    {
        //_rigibBody.velocity = Vector2.zero;
        _deployPosition = transform.position;
        _cCollider.enabled = false;
        _spriteRenderer.enabled = false;
        //_spriteRenderer.sprite = _sprites[1];
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
        _spriteRenderer.enabled = true;
        //_spriteRenderer.sprite = _sprites[0];
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
        _iEventChannel.onReturnButtonPressed += ReturnWatchBeforeDeployingChronoZone;
    }

    private void OnDestroy()
    {
        _cChannel.onChronoZoneDeploy -= DestroyWatch;
        _cChannel.onCheckPointRestore -= RestoreWatch;
        _iEventChannel.onReturnButtonPressed -= ReturnWatchBeforeDeployingChronoZone;
    }
}