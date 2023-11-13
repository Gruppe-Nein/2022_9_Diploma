using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void ShowHidden()
    {
        _animator.SetBool("fadeOut", true);

    }
}
