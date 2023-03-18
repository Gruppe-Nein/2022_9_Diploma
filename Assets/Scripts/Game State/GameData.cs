using UnityEngine;

public class GameData
{
    private Vector3 _playerPosition;
    private int _sceneToLoad;
    private GameState _stateToLoad;
    private GameDifficulty _gameDifficulty;
    private int _playerHealth;

    public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }
    public int SceneToLoad { get => _sceneToLoad; set => _sceneToLoad = value; }
    public GameState StateToLoad { get => _stateToLoad; set => _stateToLoad = value; }
    public GameDifficulty GameDifficulty { get => _gameDifficulty; set => _gameDifficulty = value; }
    public int PlayerHealth { get => _playerHealth; set => _playerHealth = value; }

    public GameData()
    {
        SceneToLoad = 0;
        StateToLoad = GameState.MainMenu;
        PlayerPosition = new Vector3();
        GameDifficulty = GameDifficulty.Easy;
    }

    public void SetDifficilty(GameDifficulty difficulty)
    {
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                Debug.Log("Chosen difficulty: easy");
                PlayerHealth = 2;
                break;
            case GameDifficulty.Normal:
                Debug.Log("Chosen difficulty: normal");
                PlayerHealth = 1;
                break;
        }
        GameDifficulty = difficulty;
    }

    public bool GetDamage(int damage)
    {
        PlayerHealth -= damage;
        if (PlayerHealth <= 0)
        {            
            PlayerHealth = 0;
            return true; // player is dead
        }
        else return false; // player is alive
    }
}
