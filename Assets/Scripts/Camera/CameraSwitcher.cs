using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _animator; // Require to control Cinemachine State Driven camera.
    [SerializeField] private CinemachineVirtualCamera camera1; // Staircase camera.
    [SerializeField] private CinemachineVirtualCamera camera2; // Start room camera.
    [SerializeField] private GameObject _player;

    private Collider2D playerCollider;
    private CinemachineConfiner confiner;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        playerCollider = _player.GetComponent<Collider2D>();
        confiner = camera2.GetComponent<CinemachineConfiner>();
    }

    private void Update()
    {
        // Check if the player has moved out of the view of the start room camera's Confiner.
        if (IsPlayerWithinConfiner())
        {
            _animator.SetBool("SwitchRoom", false);
            
        } else
        {
            _animator.SetBool("SwitchRoom", true);
        }
    }

    private bool IsPlayerWithinConfiner()
    {        
        Bounds cameraBounds = confiner.m_BoundingShape2D.bounds;
        return cameraBounds.Contains(playerCollider.bounds.center);
    }
}
