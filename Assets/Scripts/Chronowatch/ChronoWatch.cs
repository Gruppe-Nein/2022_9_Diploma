using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChronoWatch : MonoBehaviour
{
    #region COMPONENTS    
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region REFERENCES
    [SerializeField] private CWatchProjectile _watchProjectile;
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    #region LOCAL PARAMETERS
    private bool _onCooldown;
    private Vector3 _mousePosition;
    private Vector3 _aimDirection;
    private bool _isFacingRight;
    #endregion

    void Start()
    {
        _onCooldown = false;
        _isFacingRight = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChronoWatchAiming();

        if (gameObject.transform.parent.localScale.x != 0)
            CheckDirectionToFace(gameObject.transform.parent.localScale.x > 0);
    }
    
    private void ChronoWatchAiming()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _mousePosition.z = Camera.main.nearClipPlane;
        _aimDirection = (_mousePosition - transform.position).normalized;
        float rotAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotAngle);
    }

    public void ShootWatchProjectile(InputAction.CallbackContext context)
    {
        if (context.performed && !_onCooldown)
        {
            _cChannel.WatchProjectileDeploy(true);
            Instantiate(_watchProjectile, transform.position, _rotationAim);
        }
    }

    private void DisableWatch(bool isDeployed)
    {
        if (isDeployed)
        {
            _onCooldown = true;
            _spriteRenderer.enabled = false;
        }
        else
        {
            _onCooldown = false;
            _spriteRenderer.enabled = true;
        }
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
            Turn();
    }

    private void Turn()
    {
        _spriteRenderer.flipX = _isFacingRight;
        _spriteRenderer.flipY = _isFacingRight;

        _isFacingRight = !_isFacingRight;
    }

    private void OnEnable()
    {
        _cChannel.onWatchProjectileDeploy += DisableWatch;
    }

    private void OnDisable()
    {
        _cChannel.onWatchProjectileDeploy -= DisableWatch;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_mousePosition, 0.5f);
    }
}
