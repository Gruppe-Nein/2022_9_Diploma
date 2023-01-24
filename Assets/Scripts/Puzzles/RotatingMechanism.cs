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
        //TODO CHANGE NAMES
        if (isActive && transform.parent.name == "ChronoZone(Clone)")
        {
            _defaultSpeed = 0;
        }
        else if (!isActive && transform.parent.name == "MazePart")
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
    }
    #endregion
}
