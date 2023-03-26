using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue ISO", menuName = "Dialog System/Dialogue Lines")]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue lines with actor infor")]
    [SerializeField] private DialogueLine[] DialogueText;

    public DialogueLine GetLineByIndex(int index)
    {
        return DialogueText[index];
    }

    public int GetLength()
    {
        return DialogueText.Length - 1;
    }
}
