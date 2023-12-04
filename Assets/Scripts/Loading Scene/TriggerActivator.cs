using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject _levelTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _levelTrigger.SetActive(true);
        }
    }
}
