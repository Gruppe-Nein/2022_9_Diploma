using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSurpise : MonoBehaviour
{
    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    private bool _isStopped;
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _isStopped = false;
    }

    private void Update()
    {
        if (_isStopped && m_Animator.speed > 0.1)
        {
            m_Animator.speed *= _cData.velocityFactor;
        }
        else if (_isStopped && m_Animator.speed < 0.1)
        {
            m_Animator.speed = 0;
        }
    }

    #region CANNON TIME ZONE BEHAVIOR
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = false;
            m_Animator.speed = 1f;
        }
    }
    #endregion
}
