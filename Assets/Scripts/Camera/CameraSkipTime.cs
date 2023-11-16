using UnityEngine;

public class CameraSkipTime : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetBool("skipOn", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetBool("skipOn", false);
            skipOff();
        }
    }

    private void skipOff()
    {
        m_Animator.SetBool("skipOff", true);
    }
}
