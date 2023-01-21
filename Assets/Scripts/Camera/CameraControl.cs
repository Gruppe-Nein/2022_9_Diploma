using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject _cinemaVM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _cinemaVM.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _cinemaVM.SetActive(true);
    }
}
