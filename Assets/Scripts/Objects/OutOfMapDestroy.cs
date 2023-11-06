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
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(10);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPhys"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPhys"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(10);
        }
    }
}
