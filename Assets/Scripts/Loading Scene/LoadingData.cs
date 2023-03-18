using UnityEngine;

[CreateAssetMenu(menuName = "Loading Data")]
public class LoadingData : ScriptableObject
{
    [Header("Index of the scene to load")]
    public int sceneToLoad;

    [Space(5)]
    [Header("State to load")]
    public GameState stateToLoad;
}
