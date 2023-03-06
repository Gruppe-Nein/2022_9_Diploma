using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    
    [SerializeField] private GameObject _pauseMenu;
    private static bool _isPaused;
    private PlayerInput _pauseInput;    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        GameManager.OnGameStateChanged += GameStateChange;
        _pauseInput = GetComponent<PlayerInput>();
        _isPaused = false;
    }

    private void GameStateChange(GameState state)
    {
        if (state == GameState.Gameplay)
        {
            _pauseInput.ActivateInput();
        }
        else
        {
            _pauseInput.DeactivateInput();
        }
        ResumeGame();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChange;
    }

    public void DeterminePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isPaused)
            {
                Debug.Log("RESUME GAME");
                ResumeGame();
            }
            else
            {
                Debug.Log("PAUSE GAME");
                PauseGame();
            }
        }
    }

    public bool CheckPause()
    {
        return _isPaused;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;        
        _isPaused = true;
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("RESUME GAME TRIGGERED");
        Time.timeScale = 1;        
        _isPaused = false;
        _pauseMenu.SetActive(false);
    }
}
