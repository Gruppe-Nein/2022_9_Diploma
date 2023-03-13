using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private TMP_Text _continueText;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private GameObject _controlMenu;

    private MenuCounter _menuCounter;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

    private void Awake()
    {
        _menuCounter = MenuCounter.Menu;
        //Debug.Log("Current Counter: " + _menuCounter);
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
                //Debug.Log("Current Counter: " + _menuCounter);
                _optionMenu.SetActive(false);
                _mainMenu.SetActive(true);
            }
            else if (_menuCounter == MenuCounter.Control)
            {
                _menuCounter = MenuCounter.Option;
                //Debug.Log("Current Counter: " + _menuCounter);
                _controlMenu.SetActive(false);
                _optionMenu.SetActive(true);
            }
        }
    }

    public void CounterUp()
    {
        _menuCounter++;
        //Debug.Log("Current Counter: " + _menuCounter);
    }

    public void CounterDown()
    {
        _menuCounter--;
        //Debug.Log("Current Counter: " + _menuCounter);
    }

    public void NewGame()
    {       
        _loadingData.sceneToLoad = SceneIndex.LEVEL_1;
        _loadingData.stateToLoad = GameState.Gameplay;
        GameEventSystem.Instance.NewGame();
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
    }

    public void Continue()
    {
        GameEventSystem.Instance.LoadData();
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
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
    }
}

