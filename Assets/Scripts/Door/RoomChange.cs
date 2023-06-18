using Cinemachine;
using System.Collections;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] GameObject To;
    [SerializeField] GameObject CameraBounds;
    [SerializeField] CinemachineConfiner cam;
    [SerializeField] private bool _cannotUse;

    [SerializeField] private Animator _fadeAnimator;

    [SerializeField] private ChronoEventChannel _cChannel;
    private bool _changing;

    private void Start()
    {
        _cChannel.onRoomChanging += ChangeRoom;
        _changing = false;
    }

    private void OnDisable()
    {
        _cChannel.onRoomChanging -= ChangeRoom;
    }

    private void ChangeRoom(bool changing)
    {
        _changing = changing;
    }

    private IEnumerator FadeTransition(Collider2D player)
    {
        yield return new WaitUntil(() => _changing == true);

        /*while (_changing == false)
        {
            yield return null;
        }*/

        if (_changing)
        {            
            player.transform.position = To.transform.GetChild(0).position;
        }               
        
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !_cannotUse)
        {
            _fadeAnimator.SetBool("Start", true);
            StartCoroutine(FadeTransition(collision));
            //collision.transform.position = To.transform.GetChild(0).position;
            //cam.m_BoundingShape2D = CameraBounds.GetComponent<PolygonCollider2D>();            
        }
    }
}
