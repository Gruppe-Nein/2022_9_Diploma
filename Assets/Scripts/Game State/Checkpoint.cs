using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer _sr { get; private set; }
    private Animator _spriteAnimator;
    private bool isEnabled = false;
    
    void Start()
    {
        _spriteAnimator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isEnabled)
        {
            isEnabled = true;
            _spriteAnimator.SetTrigger("Activated");
            GameEventSystem.Instance.SaveData();
            GameEventSystem.Instance.PlayerRestoreHealthCheckpoint(1);
        }
    }

    public bool GetIsEnabled
    {
        get { return isEnabled; }
    }
}
