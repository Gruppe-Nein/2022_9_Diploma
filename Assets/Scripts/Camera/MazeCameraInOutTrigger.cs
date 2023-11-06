using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCameraInOutTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeEncounter(false, true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeEncounter(false, false);
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeEncounter(false, true);
        }
    }
}
