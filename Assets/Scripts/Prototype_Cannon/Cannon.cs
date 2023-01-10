using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Cannonball _cannonball;
    [SerializeField] private ChronoData _cData;
    private float _cooldown;
    private float _nextTime;
    private bool _isShooting;

    private void Start()
    {
        _cooldown = 4f;
        _nextTime = Time.time + _cooldown;
        _isShooting = true;
    }

    private void Update()
    {
        if (_isShooting && Time.time > _nextTime)
        {
            Cannonball prefab = Instantiate(_cannonball, _firePoint.position, _firePoint.rotation);
            _nextTime = Time.time + _cooldown;
        }
    }

    private void OnEnable()
    {
        _cData.OnChronoZoneActive += StopShoot;
    }

    private void OnDisable()
    {
        _cData.OnChronoZoneActive -= StopShoot;
    }

    private void StopShoot(bool isActive)
    {
        if (isActive && transform.parent != null)
        {
            if (transform.parent.name == "ChronoZone(Clone)")
            {
                _isShooting = false;
            }            
        } 
        else
        {
            _isShooting = true;
        }        
    }
}
