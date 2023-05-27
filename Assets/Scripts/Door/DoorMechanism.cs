using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OpenDoor()
    {
        _animator.SetBool("isOpen", true);
        _animator.SetBool("firstOpening", true);
    }

    public void CloseDoor()
    {
        _animator.SetBool("isOpen", false);
    }
}
