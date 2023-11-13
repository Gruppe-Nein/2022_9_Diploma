using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFirstViewTrigger : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeFirstView(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeFirstView(false);
        }
    }
}
