using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    //[SerializeField] private Animator _animator;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider2D;
    [SerializeField] private Sprite _doorClosed;
    [SerializeField] private Sprite _doorOpened;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        _spriteRenderer.sprite = _doorOpened;
        _collider2D.enabled = false;
        //_animator.SetBool("isOpen", true);
        //_animator.SetBool("firstOpening", true);
    }

    public void CloseDoor()
    {
        _spriteRenderer.sprite = _doorClosed;
        _collider2D.enabled = true;
        //_animator.SetBool("isOpen", false);
    }
}
