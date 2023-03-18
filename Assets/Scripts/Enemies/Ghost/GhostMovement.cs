using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostBrain))]
public class GhostMovement : MonoBehaviour
{
    private GhostBrain _gb;
    
    private void Awake()
    {
        _gb = GetComponent<GhostBrain>();
    }

    public void Move(float speed, GameObject player)
    {
        Vector2 target = player.transform.position;
        Vector2 newPos = Vector2.MoveTowards(_gb.rb.position, target, speed * Time.deltaTime);
        _gb.rb.MovePosition(newPos);
    }
}
