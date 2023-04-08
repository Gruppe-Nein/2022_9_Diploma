using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private Rock _rock;

    #region PARAMETERS
    [SerializeField] private float _delay;
    private Vector3 _offset = new Vector3 (0, -2, 0);
    // private float _nextTime;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnRock());
        
        // _nextTime = Time.time + _cooldown;
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
        Instantiate(_rock, transform.position + _offset, transform.rotation);
    }
}
