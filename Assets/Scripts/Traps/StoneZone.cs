using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneZone : MonoBehaviour
{
    [SerializeField] private StoneTrap _trap;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            Vector3 dir = (collision.gameObject.transform.position - _trap.transform.position).normalized;
/*            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir *= Vector2.right;
            }
            else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                dir *= Vector2.up;
            }*/
            _trap.setFollow(true, dir);
        }
        
    }
}
