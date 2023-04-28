using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggybank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            DestroyPiggyBank();
        }
    }

    private void DestroyPiggyBank()
    {
        Destroy(gameObject);
    }
}
