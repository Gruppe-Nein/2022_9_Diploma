using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostBrain))]
public class GhostMovement : MonoBehaviour
{
    private GhostBrain _gb;
    private Vector2 DestPosition;

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

    public void AttackCharge(Vector2 destPosition)
    {
        DestPosition = destPosition;
        Vector2 newPos = Vector2.MoveTowards(_gb.rb.position, destPosition, _gb.ChargeSpeed * Time.deltaTime);
        _gb.rb.MovePosition(newPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(DestPosition, 3f);
    }
}
