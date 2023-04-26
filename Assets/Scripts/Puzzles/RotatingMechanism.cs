using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RotatingMechanism : MonoBehaviour
{
    #region PARAMETERS
    [SerializeField] private float _rotSpeed;
    private float _velocityController;
    private bool _isStopped;
    private Vector3 _rotCenter = new Vector3(0,0,-1);
    #endregion

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    void Start()
    {
        _velocityController = 1;
        _isStopped = false;
    }

    void Update()
    {
        if (_isStopped && _velocityController > 0.1)
        {
            _velocityController *= _cData.velocityFactor;
        }
        else if (_isStopped && _velocityController < 0.1)
        {
            _velocityController = 0;
        }
        transform.RotateAround(transform.position, _rotCenter, Time.deltaTime * _rotSpeed * _velocityController);
    }

    #region MECHANISM TIME ZONE BEHAVIOR
    //private void StopRotation(bool isActive)
    //{
    //    if (isActive)
    //    {
    //        _defaultSpeed = 0;
    //    }
    //    else {
    //        _defaultSpeed = _rotSpeed;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = true;
            //StopRotation(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = false;
            _velocityController = 1;
            //StopRotation(false);
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
