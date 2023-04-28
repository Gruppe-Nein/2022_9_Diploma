using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingZone : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for communication between zone and cannon within this zone.")]
    [SerializeField] FollowCannonEventChannel _followEventChannel;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _followEventChannel.EnterZoneFollow(true, collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _followEventChannel.EnterZoneFollow(false, collision.gameObject.transform);
        }
    }
}
