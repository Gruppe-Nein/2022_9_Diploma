using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer _sr { get; private set; }
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _sr.color = Color.green;
            GameEventSystem.Instance.SaveData();
        }
    }
}
