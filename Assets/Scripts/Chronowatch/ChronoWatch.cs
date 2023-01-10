using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoWatch : MonoBehaviour
{
    [SerializeField] private ChronoWatchTargeting _target;
    [SerializeField] private CWatchProjectile _watchProjectile;
    [SerializeField] private ChronoData _cData;
    [SerializeField] private Transform _watchPlace;
    private bool _onCooldown;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetDirection;

    void Start()
    {
        _onCooldown = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FollowPlayer();

        _targetDirection = (_target.transform.position - transform.position).normalized;
        float rotZ = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(rotZ, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 50f);

        if (Input.GetButtonDown("Fire1") && !_onCooldown)
        {
            Shoot();
        }
    }

    private void FollowPlayer()
    {
        float time = 0f;

        while (transform.position != _watchPlace.position)
        {
            transform.position = Vector2.Lerp(transform.position, _watchPlace.position, (time / Vector2.Distance(transform.position, _watchPlace.position)) * 2f);
            time += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        _cData.onChronoZoneDeploy += DisableWatch;
    }

    private void OnDisable()
    {
        _cData.onChronoZoneDeploy -= DisableWatch;
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

    private void Shoot()
    {
        _cData.ChronoZoneDeploy(true);
        Instantiate(_watchProjectile, transform.position, transform.rotation);
    }
}
