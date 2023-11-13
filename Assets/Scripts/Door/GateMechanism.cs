using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMechanism : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void OpenGate()
    {
        _animator.SetBool("isOpening", true);

    }

    public void CloseGate()
    {

    }
}
