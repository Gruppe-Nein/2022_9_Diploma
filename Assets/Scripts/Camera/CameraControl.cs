using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineVirtualCamera camera3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera1.Priority = 5;
        camera2.Priority = 15;
        camera3.Priority = 10;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        camera1.Priority = 15;
        camera2.Priority = 5;
        camera3.Priority = 10;
    }
}
