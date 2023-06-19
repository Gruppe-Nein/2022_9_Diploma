using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintPopup : MonoBehaviour
{
    [SerializeField] GameObject _spriteToAnimate;
    private Animator _spriteAnimator;
    private void Start()
    {
        _spriteAnimator = _spriteToAnimate.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteAnimator.SetTrigger("ShowHint");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteAnimator.SetTrigger("HideHint");
        }
    }
}
