using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentGameState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }            
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetGameState(GameState.MainMenu);
        Debug.Log("Start game state => " + CurrentGameState);
    }

    public void SetGameState(GameState newGameState)
    {
        if (CurrentGameState == newGameState)
        {
            return;
        }       
        CurrentGameState = newGameState;
        Debug.Log("Current game state => " + CurrentGameState);
        OnGameStateChanged?.Invoke(CurrentGameState);
    }
}