using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour
{
    [SerializeField] private Transform _anotherPortal;

    public Transform getAnotherPortal()
    {
        return _anotherPortal;
    }
}
