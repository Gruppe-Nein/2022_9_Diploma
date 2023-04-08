using UnityEngine;
using UnityEngine.InputSystem;

public class Teleportal : MonoBehaviour
{
    #region SCRIPTABLE OBJECT
    [Tooltip("ISO channel for listenning for the Player Input")]
    [SerializeField] private InputEventChannel _inputEventChannel;
    #endregion

    #region REFERENCES
    [Tooltip("Reference to another portal")]
    [SerializeField] private Teleportal _anotherPortal;
    #endregion

    #region PARAMETERS
    [Tooltip("Check if a player cannot use this portal")]
    [SerializeField] private bool _restrictPlayer;
    private bool _canBePushed = false;
    private bool _isTeleporting = false;
    // Temporal solution ? to allow teleportaion if button is pressed
    private ITeleportable _player;
    #endregion

    private void Start()
    {
        _inputEventChannel.onInteractButtonPressed += UsePortal;
    }

    private void OnDestroy()
    {
        _inputEventChannel.onInteractButtonPressed -= UsePortal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ITeleportable tObject))
        {
            if(_restrictPlayer && !collision.gameObject.CompareTag("Player"))
            {
                StartTeleportaion(tObject);
            }
            else if (!_restrictPlayer && collision.CompareTag("Player"))
            {
                _canBePushed = true;
                _player = tObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canBePushed = false;
        }
    }

    #region GENERAL METHODS
    /// <summary>
    /// Method to start teleportation with collided object that has ITeleportable interface
    /// </summary>
    private void StartTeleportaion(ITeleportable tObject)
    {
        if (!_isTeleporting)
        {
            _anotherPortal._isTeleporting = true;
            tObject.Teleport(_anotherPortal.transform.position);
        }
        _isTeleporting = false;
    }
    /// <summary>
    /// Method to start teleportaion on players pressing interactable button
    /// </summary>
    private void UsePortal(InputAction.CallbackContext context)
    {
        if (_canBePushed && context.performed)
        {
            StartTeleportaion(_player);
        }
    }
    #endregion
}