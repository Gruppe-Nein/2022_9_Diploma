using UnityEngine;

public class GameData
{
    public Vector3 playerPosition;
    public int sceneToLoad;
    public GameState stateToLoad;
    public GameDifficulty gameDifficulty;
    public int playerHealth;
    public enum GameDifficulty
    {
        Easy = 0,
        Normal = 1
    }
    public GameData()
    {
        sceneToLoad = 0;
        stateToLoad = GameState.MainMenu;
        playerPosition = new Vector3();
        gameDifficulty = GameDifficulty.Easy;
        //playerHealth = 2;
    }

    public void SetDifficilty(GameDifficulty difficulty)
    {
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                Debug.Log("Chosen difficulty: easy");
                playerHealth = 2;
                break;
            case GameDifficulty.Normal:
                Debug.Log("Chosen difficulty: normal");
                playerHealth = 1;
                break;
        }
        gameDifficulty = difficulty;
    }

    public bool GetDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            return true; // player is dead
        }
        else return false; // player is alive
    }
}
