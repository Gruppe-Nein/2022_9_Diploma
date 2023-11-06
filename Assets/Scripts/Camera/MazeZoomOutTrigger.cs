using UnityEngine;

public class MazeZoomOutTrigger : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeEncounter(true, false);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.MazeEncounter(false, false);
        }        
    }
}
