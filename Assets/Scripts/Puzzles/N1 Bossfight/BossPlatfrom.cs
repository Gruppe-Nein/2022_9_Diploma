using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPlatfrom : MonoBehaviour
{
    private List<GameObject> cogwheels = new List<GameObject>();
    void Start()
    {
        SetCogwheels();
    }

    private void SetCogwheels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).CompareTag("BossCogwheel"))
                cogwheels.Add(transform.GetChild(i).gameObject);
        }
        /*foreach (var item in cogwheels)
        {
            Debug.Log(item.name);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("xdd");
        if (collision.gameObject.CompareTag("ChronoZone"))
        {
            Debug.Log("xxd");
            DeactiveCogwheel();
        }
    }

    private void DeactiveCogwheel()
    {
        Debug.Log("asd");
        var cogwheel = cogwheels.First();
        cogwheel.SetActive(false);
        cogwheels.Remove(cogwheel);
    }
}
