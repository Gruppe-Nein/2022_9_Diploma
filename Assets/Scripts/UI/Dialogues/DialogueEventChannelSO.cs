using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for talk interaction events.
/// Example: start talking to an actor passed as paramater
/// </summary>

[CreateAssetMenu(fileName = "Dialogue Event Channel", menuName = "Dialog System/Dialogue Event Channel")]
public class DialogueEventChannelSO : ScriptableObject
{
    public event UnityAction<DialogueSO> OnDialogueEventRaised;

    public void DialogueRaiseEvent(DialogueSO dialogue)
    {
        OnDialogueEventRaised?.Invoke(dialogue);
    }
}