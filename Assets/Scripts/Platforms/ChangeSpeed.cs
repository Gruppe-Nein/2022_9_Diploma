using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    [SerializeField] private MoveOnEnterPlatform _moveOnEnterPlatform;
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {

            _moveOnEnterPlatform.setSpeed(_speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _moveOnEnterPlatform.setSpeed(3);
        }
    }
}
