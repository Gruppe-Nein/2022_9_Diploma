using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void Start()
    {
        GameEventSystem.Instance.OnMazeEncounter += ChangeMazeCamera;
    }

    private void ChangeMazeCamera(bool zoomOut, bool inMaze)
    {
        _animator.SetBool("zoomOut", zoomOut);
        _animator.SetBool("inMaze", inMaze);
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.OnMazeEncounter -= ChangeMazeCamera;
    }
}
