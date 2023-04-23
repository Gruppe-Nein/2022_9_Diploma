using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGate : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for communication between different parts of the single Puzzle.")]
    [SerializeField] CodePuzzleEventChannel _codePuzzleEventChannel;
    #endregion

    #region PARAMETERS
    //TEMPORAL PARAMETER
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    #endregion

    private void Start()
    {
        _codePuzzleEventChannel.OnCheckCodeEvent += CheckCode;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer.color = Color.red;
    }

    private void OnDestroy()
    {
        _codePuzzleEventChannel.OnCheckCodeEvent -= CheckCode;
    }

    private void CheckCode(bool isCorrect)
    {
        if (isCorrect)
        {
            _spriteRenderer.color = Color.green;
            _boxCollider.enabled = false;
        }
        else
        {
            _spriteRenderer.color = Color.red;
            _boxCollider.enabled = true;
        }
    }
}
