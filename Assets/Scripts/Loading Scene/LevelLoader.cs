using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    #region SCRIPTABLE OBJECTS
    [SerializeField] private LoadingData _loadingData;
    #endregion

    #region COMPONENTS
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _progressText;
    #endregion

    void Start()
    {
        StartCoroutine(LoadSceneAsynchronously(_loadingData.sceneToLoad, _loadingData.stateToLoad));        
    }

    IEnumerator LoadSceneAsynchronously(int scene, GameState state)
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);
        loadingOperation.allowSceneActivation = false;

        if (GameManager.Instance.CurrentGameState == GameState.NewLevel)
        {
            GameEventSystem.Instance.setPlayerDefaultPosition();
        }

        _progressBar.value = 0;
        float progress = 0f;

        while (!loadingOperation.isDone)
        {
            progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            _progressBar.value = progress;
            _progressText.text = progress * 100f + "%";
            if (progress >= 0.9f)
            {
                GameManager.Instance.SetGameState(state);
                loadingOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}




