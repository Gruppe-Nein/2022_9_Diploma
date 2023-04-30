using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggybank : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            GameEventSystem.Instance.PiggybankDestroy(gameObject);
        }
    }
}
