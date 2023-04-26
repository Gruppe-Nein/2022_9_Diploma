using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingSaw : MonoBehaviour
{
    #region SAW MOVEMENT END POINTS
    [SerializeField] private Transform[] _positions;
    #endregion

    private float count = 0.0f;
    private Vector2 positionMid;
    private Animator m_Animator;

    /// <summary>
    /// 1) paramA = true, paramB = true     : direction up.
    /// 2) paramA = false, paramB = false   : direction down.
    /// 3) paramA = true, paramB = false    : direction right.
    /// 4) paramA = false, paramB = true    : direction left.
    /// </summary>
    [SerializeField] private bool _paramA;
    [SerializeField] private bool _paramB;

    #region SCRIPTABLE OBJECTS
    [SerializeField] private ChronoData _cData;
    #endregion

    #region LOCAL PARAMETERS
    private float _velocityController;
    private bool _isStopped;

    [SerializeField] private float _arcHeight = 1.0f;
    #endregion

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (_paramA && _paramB)
        {
            positionMid = _positions[0].position + ((_positions[1].position - _positions[0].position) * 0.5f) + Vector3.up * _arcHeight;
        }
        else if (!_paramA && !_paramB)
        {
            positionMid = _positions[0].position + ((_positions[1].position - _positions[0].position) * 0.5f) + Vector3.down * _arcHeight;
        }
        else if (_paramA && !_paramB)
        {
            positionMid = _positions[0].position + ((_positions[1].position - _positions[0].position) * 0.5f) + Vector3.right * _arcHeight;
        }
        else if (!_paramA && _paramB)
        {
            positionMid = _positions[0].position + ((_positions[1].position - _positions[0].position) * 0.5f) + Vector3.left * _arcHeight;
        }

        Debug.Log(positionMid);

        _velocityController = 1;
        _isStopped = false;
    }

    private void Update()
    {
        if (_isStopped && _velocityController > 0.1)
        {
            _velocityController *= _cData.velocityFactor;
            m_Animator.speed *= _cData.velocityFactor;
        }
        else if (_isStopped && _velocityController < 0.1)
        {
            _velocityController = 0;
            m_Animator.speed = 0;
        }

        if (count < 1.0f)
        {
            count += 0.5f * Time.deltaTime * _velocityController;

            Vector3 m1 = Vector3.Lerp(_positions[0].position, positionMid, count);
            Vector3 m2 = Vector3.Lerp(positionMid, _positions[1].position, count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        CheckOverlapCircle();
    }

    private void CheckOverlapCircle()
    {
        if ((transform.position - _positions[1].position).magnitude <= 0.1)
        {
            transform.position = _positions[0].position;
            count = 0;
        }
    }

    #region PLATFORM TIME ZONE BEHAVIOR
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _isStopped = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.PlayerTakeDamage(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            _velocityController = 1;
            _isStopped = false;
            m_Animator.speed = 1;
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_positions[0].position, 0.2f);
        Gizmos.DrawWireSphere(_positions[1].position, 0.2f);
    }
}
