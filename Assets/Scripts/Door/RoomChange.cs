using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] GameObject To;
    [SerializeField] GameObject CameraBounds;
    [SerializeField] CinemachineConfiner cam;
    [SerializeField] private bool _cannotUse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !_cannotUse)
        {
            Debug.Log("asdas");
            collision.transform.position = To.transform.GetChild(0).position;
            cam.m_BoundingShape2D = CameraBounds.GetComponent<PolygonCollider2D>();
        }
    }
}
