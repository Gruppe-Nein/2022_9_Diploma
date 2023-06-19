using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConnectedTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO[] _dialogues;
    [SerializeField] private DialogueEventChannelSO _dChannel;

    private int _counter;

    private void Start()
    {
        _counter = 0;
    }

    public void StartDialogue()
    {
        GameManager.Instance.SetGameState(GameState.Dialogue);
        _dChannel.DialogueRaiseEvent(_dialogues[_counter]);
        _counter++;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDialogue();
        }
    }*/
}
