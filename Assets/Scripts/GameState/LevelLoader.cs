using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAsynchronously(LoadingData.sceneToLoad, LoadingData.stateToLoad));        
    }

    IEnumerator LoadSceneAsynchronously(int scene, GameState state)
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);
        loadingOperation.allowSceneActivation = false;

        float progress = 0f;
        while (!loadingOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, loadingOperation.progress, Time.deltaTime);
            // TO DO = progress bar;
            if (progress >= 0.9f)
            {
                GameManager.Instance.SetGameState(state);
                loadingOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}




