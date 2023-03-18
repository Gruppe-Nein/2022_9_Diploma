using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private GhostBrain _gb;
    
    private void Awake()
    {
        _gb = GetComponent<GhostBrain>();
    }

    public void Move(float speed)
    {
        Vector2 target = _gb.player.transform.position;
        Vector2 newPos = Vector2.MoveTowards(_gb.rb.position, target, speed * Time.deltaTime);
        _gb.rb.MovePosition(newPos);
    }
}
