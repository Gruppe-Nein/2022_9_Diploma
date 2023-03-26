using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RotatingMechanism : MonoBehaviour
{
    #region PARAMETERS
    [SerializeField] private float _rotSpeed;
    private float _defaultSpeed;
    private Vector3 _rotCenter = new Vector3(0,0,-1);
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoEventChannel _cChannel;
    #endregion

    void Start()
    {
        _defaultSpeed = _rotSpeed;
    }

    void Update()
    {
        transform.RotateAround(transform.position, _rotCenter, Time.deltaTime * _defaultSpeed);
    }

    #region MECHANISM TIME ZONE BEHAVIOR
    private void StopRotation(bool isActive)
    {
        if (isActive)
        {
            _defaultSpeed = 0;
        }
        else {
            _defaultSpeed = _rotSpeed;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopRotation(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopRotation(false);
        }
    }
    /*
    private void StopRotation(bool isActive)
    {
        if (isActive && transform.parent.name == "ChronoZone(Clone)")
        {
            _defaultSpeed = 0;
        }
        else if (!isActive && transform.parent == null)
        {
            _defaultSpeed = _rotSpeed;            
        }
    }

    private void OnEnable()
    {
        _cChannel.OnChronoZoneActive += StopRotation;
    }

    private void OnDestroy()
    {
        _cChannel.OnChronoZoneActive -= StopRotation;
    }*/
    #endregion
}
