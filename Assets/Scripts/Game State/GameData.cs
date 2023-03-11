using UnityEngine;

public class GameData
{
    public Vector3 playerPosition;
    public int sceneToLoad;
    public GameState stateToLoad;
    public GameData()
    {
        playerPosition = new Vector3();
        sceneToLoad = 0;
        stateToLoad = GameState.MainMenu;
    }
}
