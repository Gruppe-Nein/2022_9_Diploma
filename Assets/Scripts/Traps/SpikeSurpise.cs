using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSurpise : MonoBehaviour
{
    Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(1);
        }
    }

    #region CANNON TIME ZONE BEHAVIOR
    private void StopAnimation(bool isActive)
    {
        if (isActive)
        {
            m_Animator.speed = 0;
        }
        else
        {
            m_Animator.speed = 1f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopAnimation(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            StopAnimation(false);
        }
    }
    #endregion
}
