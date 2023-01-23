using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverMechanism : MonoBehaviour
{
    [Header("Door Events")]
    [SerializeField] private UnityEvent closeDoor;
    [SerializeField] private UnityEvent openDoor;

    private bool _isTriggered = false;
    private bool _canBePushed = false;

    private SpriteRenderer _sp;

    private void Awake()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_canBePushed && Input.GetKeyUp(KeyCode.E))
        {
            if (_isTriggered == false)
            {
                openDoor.Invoke();
                _isTriggered = true;
                _sp.color = Color.green;
            }
            else
            {
                closeDoor.Invoke();
                _isTriggered = false;
                _sp.color = Color.red;
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
}
