using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    #region CHECK TAG
    public bool _checkObjectIsOn = false;
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        _checkObjectIsOn = true;
    
    }

}
