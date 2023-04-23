using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Event Channel", menuName = "Player/Player Input Event Channel")]
public class InputEventChannel : ScriptableObject
{
    public event Action<InputAction.CallbackContext> onShootButtonPressed;
    public event Action<InputAction.CallbackContext> onInteractButtonPressed;

    public void ShootButtonPressed(InputAction.CallbackContext context)
    {
        onShootButtonPressed?.Invoke(context);
    }    

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        onInteractButtonPressed?.Invoke(context);
    }
}