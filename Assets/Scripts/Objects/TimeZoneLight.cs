using UnityEngine;

public class TimeZoneLight : MonoBehaviour
{
    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    #region LOCAL PARAMETERS
    private bool _isStopped;
    private Animator m_Animator;
    #endregion

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
        _isStopped = false;
    }

    void Update()
    {
        if (_isStopped)
        {
            m_Animator.speed *= _cData.velocityFactor;
        }
        else if (_isStopped)
        {
            m_Animator.speed = 0;
        }
    }

    #region PLATFORM TIME ZONE BEHAVIOR
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = false;
            m_Animator.speed = 1;
        }
    }
    #endregion
}
