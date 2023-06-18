using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField] private ChronoEventChannel _cChannel;
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void ChangeRoom()
    {
        _cChannel.ChangingRoom(true);        
    }

    private void NotChangeRoom()
    {
        _cChannel.ChangingRoom(false);
    }

    private void ResetAnimation()
    {
        m_Animator.SetBool("Start", false);
    }
}
