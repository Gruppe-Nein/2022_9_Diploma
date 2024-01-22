using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTextPopup : MonoBehaviour
{
    public GameObject TextObject;
    private Animator _textAnimator;
    private void Start()
    {
        _textAnimator = TextObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _textAnimator.SetTrigger("AnimateText");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _textAnimator.SetTrigger("FadeText");
        }
    }
}
