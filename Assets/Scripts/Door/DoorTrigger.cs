using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject PortalA;

    [SerializeField] GameObject PortalB;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(PortalB.transform.position, 1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PortalA.transform.position, PortalA.GetComponent<BoxCollider2D>().size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(PortalB.transform.position, PortalB.GetComponent<BoxCollider2D>().size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PortalA.transform.position, PortalB.transform.position);
    }
}
