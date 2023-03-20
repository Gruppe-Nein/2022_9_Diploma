using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor ISO", menuName = "Dialog System/Actor")]
public class ActorSO : ScriptableObject
{
    [Header("Name of a dialogue actor")]
    [SerializeField] private string _actor;
    [Header("Image of an actor for UI")]
    [SerializeField] private Sprite _actorImage;

    public string getName()
    {
        return _actor;
    }

    public Sprite getImage()
    {
        return _actorImage;
    }
}
