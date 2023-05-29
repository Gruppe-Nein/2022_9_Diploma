using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlickeringLight : MonoBehaviour
{
    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            _light.intensity = Random.Range(2f, 3f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}