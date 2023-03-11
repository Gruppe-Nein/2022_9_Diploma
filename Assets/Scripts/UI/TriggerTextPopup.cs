using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTextPopup : MonoBehaviour
{
    public TMP_Text textField;
    private Animator _textAnimator;
    private void Start()
    {
        _textAnimator = textField.GetComponent<Animator>();
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
