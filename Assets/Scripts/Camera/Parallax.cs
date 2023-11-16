using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxFactor;
    [SerializeField] private float _pixelsPerUnit;
    private float _startpos;

    public GameObject cam;

    void Start()
    {
        _startpos = transform.position.x;
    }

    void Update()
    {
        float distance = cam.transform.position.x * _parallaxFactor;
        Vector3 newPosition = new Vector3(_startpos + distance, transform.position.y, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, _pixelsPerUnit);
    }

    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit), Mathf.CeilToInt(locationVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
}
