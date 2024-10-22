using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAudio : MonoBehaviour
{
    #region IMPORTS
    public AudioSource _roomAudio;
    #endregion

    #region LOCAL VARIABLES

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        _roomAudio.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        _roomAudio.enabled = false;
    }
}
