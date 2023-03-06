using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoWatch : MonoBehaviour
{
    #region COMPONENTS    
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region REFERENCES
    [SerializeField] private Camera _mainCamera;
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
    #endregion

    void Start()
    {
        _onCooldown = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (PauseManager.Instance.CheckPause() == false)
        {
            ChronoWatchAiming();

            if (Input.GetButtonDown("Fire1") && !_onCooldown)
            {
                ShootWatchProjectile();
            }
        }
    }

    private void ChronoWatchAiming()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _aimDirection = (_mousePosition - transform.position).normalized;
        float rotAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotAngle);
    }

    private void ShootWatchProjectile()
    {
        _cChannel.ChronoZoneDeploy(true);
        Instantiate(_watchProjectile, transform.position, transform.rotation);
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
        _cChannel.onChronoZoneDeploy += DisableWatch;
    }

    private void OnDisable()
    {
        _cChannel.onChronoZoneDeploy -= DisableWatch;
    }
}
