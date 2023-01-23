using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GridBrushBase;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private Rock _rock;

    #region PARAMETERS
    [SerializeField] private float _delay;
    private float _nextTime;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnRock());
        
        //_nextTime = Time.time + _cooldown;
    }

    private void Update()
    {
        /*if (Time.time > _nextTime)
        {
            Instantiate(_rock, transform.position, transform.rotation);
            _nextTime = Time.time + _cooldown;
        }*/
    }

    private IEnumerator SpawnRock()
    {
        yield return new WaitForSeconds(_delay);
        Instantiate(_rock, transform.position, transform.rotation);
    }
}
