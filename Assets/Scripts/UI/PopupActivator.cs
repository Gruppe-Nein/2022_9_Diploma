using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupActivator : MonoBehaviour
{
    #region COMPONENTS
    [SerializeField] GameObject _popupWindow;
    [SerializeField] string _popupText;
    #endregion
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _popupWindow.GetComponent<PopupScript>().AddToQueue(_popupText);
        }
    }
}
