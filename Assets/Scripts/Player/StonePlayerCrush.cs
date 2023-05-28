using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlayerCrush : MonoBehaviour
{
    private bool collidedWithObjectA = false;
    private bool collidedWithObjectB = false;

    private void Update()
    {
        if (collidedWithObjectA && collidedWithObjectB)
        {
            GameEventSystem.Instance.PlayerTakeDamage(2);

            // Reset collision flags
            collidedWithObjectA = false;
            collidedWithObjectB = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StoneHead"))
        {
            collidedWithObjectA = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StoneHead"))
        {
            collidedWithObjectA = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StoneWall"))
        {
            collidedWithObjectB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StoneWall"))
        {
            collidedWithObjectB = false;
        }
    }
}
