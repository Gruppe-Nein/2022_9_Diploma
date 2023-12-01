using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AllPiggybanks : MonoBehaviour
{
    [HideInInspector] public List<GameObject> piggybanks = new List<GameObject>();
    [HideInInspector] public GameObject lastPiggybankDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        SetPiggybanks();
        GameEventSystem.Instance.OnPiggybankDestroy += DeactivePiggybank;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPiggybanks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            piggybanks.Add(transform.GetChild(i).gameObject);
        }
    }

    private void DeactivePiggybank(GameObject piggybank)
    {
        //var piggybank = piggybanks.First();
        lastPiggybankDestroyed = piggybank;
        piggybank.SetActive(false);
        piggybanks.Remove(piggybank);
    }

    public void RespawnLastPiggybank()
    {
        lastPiggybankDestroyed.SetActive(true);
        piggybanks.Add(lastPiggybankDestroyed);
    }

}
