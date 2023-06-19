using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerHelper : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField] private DialogueConnectedTrigger parent;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.enabled = false;
            parent.StartDialogue();
            //StartDialogue();
        }
        
    }
}
