using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private DialogueEventChannelSO _dChannel;
    [SerializeField] private bool _isLoopable;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void StartDialogue()
    {
        GameManager.Instance.SetGameState(GameState.Dialogue);
        _dChannel.DialogueRaiseEvent(_dialogue);

        if (_isLoopable == false)
        {
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDialogue();
        }        
    }
}
