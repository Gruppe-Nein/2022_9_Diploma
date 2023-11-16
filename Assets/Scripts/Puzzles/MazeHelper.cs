using UnityEngine;

public class MazeHelper : MonoBehaviour
{
    [SerializeField] RotatingMechanism _mechanism;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            collision.transform.RotateAround(transform.position, new Vector3(0, 0, -1), Time.deltaTime * 5f * _mechanism.getVelocityController());
        }

        if (collision.gameObject.CompareTag("CWatchProjectile"))
        {
            collision.transform.RotateAround(transform.position, new Vector3(0, 0, -1), Time.deltaTime * 5f * _mechanism.getVelocityController());
        }
    }
}
