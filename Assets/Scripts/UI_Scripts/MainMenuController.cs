using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    #region COMPONENTS
    [SerializeField] private Button _continueButton;
    [SerializeField] private TMP_Text _continueText;
    #endregion

    #region MENU PANELS
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private GameObject _difficultyMenu;
    #endregion

    #region PARAMETERS
    private MenuCounter _menuCounter;
    #endregion    

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    [SerializeField] private List<SceneIndex> _sceneIndexes;
    #endregion

    private void Awake()
    {
        _menuCounter = MenuCounter.Menu;
    }

    private void Start()
    {
        if (File.Exists(Application.dataPath + "/../save.xml"))
        {
            _continueButton.enabled = true;
        }
        else
        {
            _continueButton.enabled = false;
            Color color = _continueText.color;
            color.a = 0.5f;
            _continueText.color = color;
        }
    }

    public void DetermineSection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_menuCounter == MenuCounter.Option)
            {
                _menuCounter = MenuCounter.Menu;
                _optionMenu.SetActive(false);
                _mainMenu.SetActive(true);
            }
            else if (_menuCounter == MenuCounter.Control)
            {
                _menuCounter = MenuCounter.Option;
                _controlMenu.SetActive(false);
                _optionMenu.SetActive(true);
            }
            else if (_menuCounter == MenuCounter.Diffuclty)
            {
                _menuCounter = MenuCounter.Menu;
                _difficultyMenu.SetActive(false);
                _mainMenu.SetActive(true);
            }
        }
    }

    public void SetUpMenuCounter(int menuCounter)
    {
        if (menuCounter < 0 || Enum.GetValues(typeof(MenuCounter)).Cast<MenuCounter>().Distinct().Count() < menuCounter)
        {
            Debug.LogError("Menu Counter out of bound");
            _menuCounter = 0;
            return;
        }
        _menuCounter = (MenuCounter)menuCounter;
    }

    public void NewGame(int difficulty)
    {       
        _loadingData.sceneToLoad = _sceneIndexes[1].levelIndex;
        _loadingData.stateToLoad = GameState.Gameplay;
        GameEventSystem.Instance.NewGame(difficulty);
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(_sceneIndexes[0].levelIndex); ;
    }

    public void Continue()
    {
        GameEventSystem.Instance.LoadData();
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(_sceneIndexes[0].levelIndex);
    }

    public void QuitGame()
    {
        //It doesn't work in Editor mode.
        Application.Quit();
    }

    private enum MenuCounter
    {
        Menu = 0,
        Option = 1,
        Control = 2,
        Diffuclty = 3
    }
}

