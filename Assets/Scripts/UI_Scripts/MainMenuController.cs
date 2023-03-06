using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        //_startButton.enabled = false;
    }

    private void Update()
    {

    }

    public void QuitGame()
    {
        //It doesn't work in Editor mode.
        Application.Quit();
    }

    public void NewGame()
    {
        LoadingData.sceneToLoad = SceneIndex.LEVEL_1;
        LoadingData.stateToLoad = GameState.Gameplay;
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
    }
}

