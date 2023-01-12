using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Cannonball _cannonball;
    [SerializeField] private ChronoEventChannel _cChannel;
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
            Instantiate(_cannonball, _firePoint.position, _firePoint.rotation);
            _nextTime = Time.time + _cooldown;
        }
    }

    #region CANNON TIME ZONE BEHAVIOR

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

    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += StopShoot;
    }

    private void OnDisable()
    {
        _cChannel.OnChronoZoneActive -= StopShoot;
    }
    #endregion
}
