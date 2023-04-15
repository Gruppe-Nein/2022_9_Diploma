using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] GameObject an;
    [SerializeField] GameObject corridor;
    [SerializeField] CinemachineConfiner cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("asdas");
            collision.transform.position = an.transform.position;
            cam.m_BoundingShape2D = corridor.GetComponent<PolygonCollider2D>();
        }
    }
}
