using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] GameObject To;
    [SerializeField] GameObject CameraBounds;
    [SerializeField] CinemachineConfiner cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("asdas");
            collision.transform.position = To.transform.GetChild(0).position;
            cam.m_BoundingShape2D = CameraBounds.GetComponent<PolygonCollider2D>();
        }
    }
}
