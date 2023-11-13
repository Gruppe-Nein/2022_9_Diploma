using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeverMechanism : MonoBehaviour
{
    [Header("Door Events")]
    [SerializeField] private UnityEvent closeDoor;
    [SerializeField] private UnityEvent openDoor;

    [SerializeField] private InputEventChannel _inputEventChannel;

    private bool _isTriggered = false;
    private bool _canBePushed = false;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _inputEventChannel.onInteractButtonPressed += activateLever;
    }

    private void activateLever(InputAction.CallbackContext context)
    {
        if (_canBePushed && context.performed)
        {
            if (_isTriggered == false)
            {
                _animator.SetBool("Activate", true);
                openDoor.Invoke();
                _isTriggered = true;
            }
            else
            {
                _animator.SetBool("Activate", false);
                closeDoor.Invoke();
                _isTriggered = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canBePushed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canBePushed = false;
        }
    }

    private void OnDisable()
    {
        _inputEventChannel.onInteractButtonPressed -= activateLever;
    }
}
