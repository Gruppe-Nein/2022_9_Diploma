using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBossTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private DialogueEventChannelSO _dChannel;

    [SerializeField] private BossPlatfrom _bossPlatfrom;
    private bool _readyToMove;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _readyToMove = false;
    }

    private void Update()
    {
        if (_readyToMove && GameManager.Instance.CurrentGameState == GameState.Gameplay)
        {
            _readyToMove = false;
            _bossPlatfrom.MoveToPosition(1);
        }
    }

    private void StartDialogue()
    {
        GameManager.Instance.SetGameState(GameState.Dialogue);
        _dChannel.DialogueRaiseEvent(_dialogue);

        _readyToMove = true;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDialogue();
        }        
    }
}
