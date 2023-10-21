using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose_Animation_Manager : MonoBehaviour
{
   
    [SerializeField] RoseBrain _rose;
    private Animator _roseAnimator;
    void Start()
    {
        _roseAnimator = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (_rose.checkState() == "IdleRoseState")
        {
            _roseAnimator.ResetTrigger("MoveToState");
            _roseAnimator.SetTrigger("IdleRoseState");
        }

        if( _rose.checkState() == "MoveToState")
        {
            _roseAnimator.ResetTrigger("IdleRoseState");
            _roseAnimator.SetTrigger("MoveToState");
            
        }    
    }
}
