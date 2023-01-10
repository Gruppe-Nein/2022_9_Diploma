using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoWatchTargeting : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private Vector2 _mousePosition;

    void Update()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePosition;
    }
}
