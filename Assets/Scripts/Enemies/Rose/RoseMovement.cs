using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMovement : MonoBehaviour
{
    private GhostBrain _gb;

    private void Awake()
    {
        _gb = GetComponent<GhostBrain>();
    }

    public void MoveToPiggybank(float speed, GameObject piggyBank)
    {
        Vector2 target = piggyBank.transform.position;
        Vector2 newPos = Vector2.MoveTowards(_gb.rb.position, target, speed * Time.deltaTime);
        _gb.rb.MovePosition(newPos);
    }
}
