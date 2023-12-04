using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlickeringLight : MonoBehaviour
{
    private Light2D _light;
    private bool _isStopped;
    [SerializeField] private float _minIntensity;
    [SerializeField] private float _maxIntensity;


    private void Start()
    {
        _isStopped = false;
        _light = GetComponent<Light2D>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (!_isStopped)
        {
            _light.intensity = Random.Range(_minIntensity, _maxIntensity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    #region PLATFORM TIME ZONE BEHAVIOR
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            Debug.Log("FLICKER STOP");
            _isStopped = true;
            StopCoroutine(Flicker());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            Debug.Log("FLICKER GO");            
            _isStopped = false;
            StartCoroutine(Flicker());
        }
    }
    #endregion
}