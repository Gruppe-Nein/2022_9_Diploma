using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupScript : MonoBehaviour
{
    public TMP_Text popupText;

    [SerializeField] GameObject window;
    private Animator popupAnimator;
   

    private Queue<string> popupQueue; 
    private Coroutine queueChecker;

    private void Start()
    {
        popupAnimator = window.GetComponent<Animator>();
        popupQueue = new Queue<string>();
    }

    public void AddToQueue(string text)
    {//parameter the same type as queue
        popupQueue.Enqueue(text);
        if (queueChecker == null)
            queueChecker = StartCoroutine(CheckQueue());
    }

    private void ShowPopup(string text)
    { //parameter the same type as queue
        window.SetActive(true);
        popupText.text = text;
        popupAnimator.Play("PopupAnimation");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));

        } while (popupQueue.Count > 0);
        window.SetActive(false);
        queueChecker = null;
    }

}