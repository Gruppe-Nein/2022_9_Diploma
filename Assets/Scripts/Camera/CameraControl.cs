using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void Start()
    {
        GameEventSystem.Instance.OnMazeEncounter += ChangeMazeCamera;
        GameEventSystem.Instance.OnMazeFirstView += ChangeFirstViewCamera;
    }

    private void ChangeMazeCamera(bool zoomOut, bool inMaze)
    {
        _animator.SetBool("zoomOut", zoomOut);
        _animator.SetBool("inMaze", inMaze);
    }

    private void ChangeFirstViewCamera(bool firstView)
    {
        _animator.SetBool("firstView", firstView);
    }

    private void OnDisable()
    {
        GameEventSystem.Instance.OnMazeEncounter -= ChangeMazeCamera;
        GameEventSystem.Instance.OnMazeFirstView -= ChangeFirstViewCamera;
    }
}
