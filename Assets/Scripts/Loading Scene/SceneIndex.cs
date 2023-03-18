using UnityEngine;

[CreateAssetMenu(menuName = "Scene Loading Index")]
public class SceneIndex : ScriptableObject
{
    [Header("Index of a level to load")]
    public int levelIndex = 0;
}


