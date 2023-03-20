using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _dialogueUI;

    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
    }

    private void GameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Dialogue)
        {
            _pauseUI.SetActive(false);
            _dialogueUI.SetActive(true);
        } else if (gameState == GameState.Pause)
        {
            _dialogueUI.SetActive(false);
            _pauseUI.SetActive(true);            
        } else if (gameState == GameState.Gameplay)
        {
            _dialogueUI.SetActive(false);
            _pauseUI.SetActive(false);
        }       
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }
}
