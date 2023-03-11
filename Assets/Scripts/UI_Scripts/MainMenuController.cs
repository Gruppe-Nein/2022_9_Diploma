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
            Debug.Log("SAVE FILE FOUND");
        }
        else
        {
            Debug.Log("SAVE NOT FILE FOUND");
            _continueButton.enabled = false;
            Color color = _continueText.color;
            color.a = 0.5f;
            _continueText.color = color;
        }
    }

    public void NewGame()
    {
        //LoadingData.sceneToLoad = SceneIndex.LEVEL_1;
        //Debug.Log("SCENE TO LOAD FROM MAIN MENU: " + LoadingData.sceneToLoad);
        //LoadingData.stateToLoad = GameState.Gameplay;
        _loadingData.sceneToLoad = SceneIndex.LEVEL_1;
        _loadingData.stateToLoad = GameState.Gameplay;
        GameManager.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadScene(SceneIndex.LOADING);
    }

    public void Continue()
    {
        Debug.Log("LOADING SAVE");
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

