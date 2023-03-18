using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGamePauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _ingameMenu;
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private SceneIndex _mainMenuIndex;

    private static bool _isPaused;
    private PlayerInput _playerInput;
    private PauseCounter _pauseCounter;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

    public void DeterminePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isPaused && _pauseCounter == PauseCounter.IngamePause)
            {
                ResumeGame();
            }
            else if (!_isPaused && _pauseCounter == PauseCounter.Gameplay)
            {
                PauseGame();
            }
            else if (_isPaused && _pauseCounter == PauseCounter.Option)
            {
                _pauseCounter = PauseCounter.IngamePause;
                //Debug.Log("Current Counter: " + _pauseCounter);
                _optionMenu.SetActive(false);
                _ingameMenu.SetActive(true);
            }
            else if (_isPaused && _pauseCounter == PauseCounter.Control)
            {
                _pauseCounter = PauseCounter.Option;
                //Debug.Log("Current Counter: " + _pauseCounter);
                _controlMenu.SetActive(false);
                _optionMenu.SetActive(true);
            }            
        }        
    }

    public void CounterUp()
    {
        _pauseCounter++;
        //Debug.Log("Current Counter: " + _pauseCounter);
    }

    public void CounterDown()
    {
        _pauseCounter--;
        //Debug.Log("Current Counter: " + _pauseCounter);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        _playerInput.SwitchCurrentActionMap("Player Controls");
        _pauseCounter = PauseCounter.Gameplay;
        //Debug.Log(_playerInput.currentActionMap.ToString());
        //Debug.Log("Current Counter: " + _pauseCounter);
        _pauseMenu.SetActive(false);
    }

    public void RestartFromCheckpoint()
    {
        GameEventSystem.Instance.LoadData();
        ResumeGame();
    }

    public void BackToMenu()
    {
        _loadingData.sceneToLoad = _mainMenuIndex.levelIndex;
        _loadingData.stateToLoad = GameState.MainMenu;
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(_mainMenuIndex.levelIndex);
    }

    public void QuitGame()
    {
        //It doesn't work in Editor mode.
        Application.Quit();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _isPaused = true;
        _playerInput.SwitchCurrentActionMap("UI Controls");
        _pauseCounter = PauseCounter.IngamePause;
        //Debug.Log("Current Counter: " + _pauseCounter);
        //Debug.Log(_playerInput.currentActionMap.ToString());
        _pauseMenu.SetActive(true);
    }

    private void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _isPaused = false;
        _pauseCounter = PauseCounter.Gameplay;
    }

    private enum PauseCounter
    {
        Gameplay = 0,
        IngamePause = 1,
        Option = 2,
        Control = 3,
    }
}
