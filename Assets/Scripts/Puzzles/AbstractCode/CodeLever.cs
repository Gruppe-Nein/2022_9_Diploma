using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Lever used in "Abstract Code" puzzle. 
/// Each one carries a code number that can be send to the Code Controller. 
/// </summary>
public class CodeLever : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for listenning for the Player Input")]
    [SerializeField] private InputEventChannel _inputEventChannel;
    [Tooltip("ISO channel for communication between different parts of the single Puzzle.")]
    [SerializeField] private CodePuzzleEventChannel _codePuzzleEventChannel;
    #endregion

    #region PARAMETERS
    [Header("Code Number:")]
    [Tooltip("Int used as part of the solution")]
    [SerializeField] private int _leverNum;
    private bool _isReady = true;
    private bool _isTriggered = false;
    private bool _canBePushed = false;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    #endregion

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        //_spriteRenderer.color = Color.red;
    }

    private void Start()
    {
        _inputEventChannel.onInteractButtonPressed += ActivateLever;
        _codePuzzleEventChannel.OnSettingLeversEvent += SetLever;
    }

    private void OnDisable()
    {
        _inputEventChannel.onInteractButtonPressed -= ActivateLever;
        _codePuzzleEventChannel.OnSettingLeversEvent -= SetLever;
    }

    #region GENERAL METHODS
    /// <summary>
    /// Based on player inpur sends event to the Code Controller to add or remove code number from the solution.
    /// </summary>
    private void ActivateLever(InputAction.CallbackContext context)
    {
        if (_isReady && _canBePushed && context.performed)
        {
            if (_isTriggered == false)
            {
                Activate();
                _codePuzzleEventChannel.CodeNumEventAdd(_leverNum);
            }
            else
            {
                Reset();
                _codePuzzleEventChannel.CodeNumEventRemove(_leverNum);
            }
        }
    }
    /// <summary>
    /// Active or deactive levers to avoid spamming
    /// </summary>
    /// <param name="isReady"></param>
    private void SetLever(bool isReady)
    {
        if (!isReady)
        {
            _isReady = false;
        }
        else
        {
            Reset();
            _isReady = true;            
        }
    }
    /// <summary>
    /// Controls visual behavior of the lever
    /// </summary>
    private void Activate()
    {
        _animator.SetBool("Activate", true);
        _isTriggered = true;
        //_spriteRenderer.color = Color.green;
    }

    private void Reset()
    {
        _animator.SetBool("Activate", false);
        _isTriggered = false;
        //_spriteRenderer.color = Color.red;
    }
    #endregion

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
}
