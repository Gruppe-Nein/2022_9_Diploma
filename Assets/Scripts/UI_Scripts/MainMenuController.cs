using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using TMPro.EditorUtilities;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private TMP_Text _continueText;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

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


}

