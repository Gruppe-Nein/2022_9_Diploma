using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePauseController : MonoBehaviour
{
    private void Start()
    {
        //_startButton.enabled = false;
    }

    private void Update()
    {

    }

    public void BackToMenu()
    {
        LoadingData.sceneToLoad = SceneIndex.MAIN_MENU;
        LoadingData.stateToLoad = GameState.MainMenu;
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
    }

    public void QuitGame()
    {
        //It doesn't work in Editor mode.
        Application.Quit();
    }
}
