using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Rigidbody2D _rb { get; private set; }
    public Transform _currentCheckpoint { get; private set; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameEventSystem.Instance.OnSaveData += SaveGame;
    }
    void SaveGame(GameData data)
    {
        data.playerPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            var cSr = collision.gameObject.GetComponent<SpriteRenderer>();
            cSr.color = Color.green;
            GameEventSystem.Instance.SaveData();
        }
    }
}
