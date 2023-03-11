using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGamePauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private static bool _isPaused;
    private PlayerInput _playerInput;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

    public void DeterminePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        _playerInput.SwitchCurrentActionMap("Player Controls");
        //Debug.Log(_playerInput.currentActionMap.ToString());
        _pauseMenu.SetActive(false);
    }

    public void RestartFromCheckpoint()
    {
        GameEventSystem.Instance.LoadData();
        ResumeGame();
    }

    public void BackToMenu()
    {
        _loadingData.sceneToLoad = SceneIndex.MAIN_MENU;
        _loadingData.stateToLoad = GameState.MainMenu;
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
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
        //Debug.Log(_playerInput.currentActionMap.ToString());
        _pauseMenu.SetActive(true);
    }

    private void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _isPaused = false;
    }
}
