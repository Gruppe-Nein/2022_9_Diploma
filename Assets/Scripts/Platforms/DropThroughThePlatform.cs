using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThroughThePlatform : MonoBehaviour
{
    #region PARAMETERS
    private Collider2D _collider;
    private bool _playerOnPlatform;

    [SerializeField] float _secondsToWaitBeforeJumpingAgain;
    #endregion
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void SetPlayerOnPlatform(Collision2D collision, bool value)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnPlatform = value;
        }
    }

    private void Update()
    {
        if(_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {

        yield return new WaitForSeconds(_secondsToWaitBeforeJumpingAgain);
        _collider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, false);

    }
}
