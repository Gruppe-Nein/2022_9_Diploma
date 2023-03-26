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
    private Quaternion _rotationAim;
    #endregion

    void Start()
    {
        _onCooldown = false;
        _rotationAim = transform.rotation;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChronoWatchAiming();
    }

    private void ChronoWatchAiming()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _mousePosition.z = Camera.main.nearClipPlane;
        _aimDirection = (_mousePosition - transform.position).normalized;
        float rotAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

        _rotationAim = Quaternion.Euler(0, 0, rotAngle);

        /*if (gameObject.transform.parent.localScale.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, rotAngle - 180f);
            return;
        }*/

        //transform.eulerAngles = new Vector3(0, 0, rotAngle);
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
