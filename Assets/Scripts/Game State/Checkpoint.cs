using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer _sr { get; private set; }
    private Animator _spriteAnimator;
    void Start()
    {
        _spriteAnimator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _spriteAnimator.SetTrigger("Activated");
            GameEventSystem.Instance.SaveData();
            GameEventSystem.Instance.PlayerRestoreHealthCheckpoint(1);
        }
    }
}
