using UnityEngine;

public class Fallable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyPhys"))
        { 
            Destroy(collision.gameObject); 
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerFallDown();
        }
    }
}
