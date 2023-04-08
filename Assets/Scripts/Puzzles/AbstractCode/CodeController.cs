using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Stores player's solution for the puzzle and checks if order of triggered levers is correct.
/// Sends player's result to the Code Gate.
/// </summary>
public class CodeController : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for communication between different parts of the single Puzzle.")]
    [SerializeField] CodePuzzleEventChannel _codePuzzleEventChannel;
    #endregion

    #region PARAMETERS
    // Stores player's solution
    private int[] _solutionCode;
    // Stores actual solution from ISO
    private int[] _actualCode;
    // Checks number of triggered levers.
    private int _counter = 0;
    #endregion

    private void Start()
    {
        _codePuzzleEventChannel.OnCodeNumEventAdd += AddCodeNum;
        _codePuzzleEventChannel.OnCodeNumEventRemove += RemoveCodeNum;

        _solutionCode = new int[_codePuzzleEventChannel.code.Length];
        _actualCode = _codePuzzleEventChannel.code;

        Debug.Log(_solutionCode.Length);        
    }

    private void OnDestroy()
    {
        _codePuzzleEventChannel.OnCodeNumEventAdd -= AddCodeNum;
        _codePuzzleEventChannel.OnCodeNumEventRemove -= RemoveCodeNum;
    }

    #region GENERAL METHODS
    /// <summary>
    /// Adds code number from the triggered lever to the solution
    /// </summary>
    private void AddCodeNum(int _leverCode)
    {
        _solutionCode[_counter] = _leverCode;
        _counter++;

        if (_counter == _solutionCode.Length)
        {
            CheckSoluion();
        }        
    }
    /// <summary>
    /// Removes code number from the triggered lever to the solution
    /// </summary>
    private void RemoveCodeNum(int _leverCode)
    {
        _solutionCode[_counter] = 0;
        _counter--;
    }
    /// <summary>
    /// Upon triggering the last levers checks if the player's solution is correct
    /// </summary>
    private void CheckSoluion()
    {
        _codePuzzleEventChannel.SetLeversEvent(false);
        for (int i = 0; i < _counter; i++)
        {
            if (_solutionCode[i] != _actualCode[i])
            {
                Debug.Log("ARR: " + i + " Checking: " + _solutionCode[i] + "::: Actual: " + _actualCode[i]);
                Debug.Log("WRONG");
                _counter = 0;
                _codePuzzleEventChannel.SetLeversEvent(true);
                return;
            }
        }
        _codePuzzleEventChannel.CheckCodeEvent(true);
    }
    #endregion
}