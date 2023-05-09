using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadingTrigger : MonoBehaviour
{
    [SerializeField] private SceneIndex _sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameEventSystem.Instance.LoadScene(_sceneIndex.levelIndex);
        }
    }
}
