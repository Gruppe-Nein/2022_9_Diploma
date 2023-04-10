using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private Rock _rock;

    #region PARAMETERS
    [SerializeField] private float _delay;
    [SerializeField] private Vector3 _offset = new Vector3 (0, -5, 0);
    [SerializeField] private bool _flip = false;
    [SerializeField] private float _degree = 0;

    // private float _nextTime;
    #endregion

    private void Start()
    {
        if (_flip)
        {
            FlipX();
        }
        Quaternion rotation = Quaternion.Euler(0, 0, _degree);

        StartCoroutine(SpawnRock(rotation));
        
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

    private void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private IEnumerator SpawnRock(Quaternion rotation)
    {
        yield return new WaitForSeconds(_delay);
        _rock = Instantiate(_rock, transform.position + _offset, rotation, transform);


    }
}
