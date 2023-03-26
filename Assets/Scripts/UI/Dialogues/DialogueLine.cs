using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [Header("Dialogue actor")]
    [SerializeField] private ActorSO _actor;
    [Header("Actor Line")]
    [SerializeField] private string _line;

    public ActorSO getActor()
    {
        return _actor;
    }

    public string getLine()
    {
        return _line;
    }
}