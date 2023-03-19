using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPhys"))
        {
            Destroy(collision.gameObject);
        }
    }
}