using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossfight : MonoBehaviour
{
    [SerializeField] private BossPlatfrom platform;
    [SerializeField] private AllPiggybanks allPiggyBanks;

    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.Instance.OnRoseReturnToStart += RespawnPiggybank;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RespawnPiggybank()
    {
        Debug.Log("LAST METHOD");
        if(platform.cogwheels.Count > allPiggyBanks.piggybanks.Count)
        {
            allPiggyBanks.RespawnLastPiggybank();
        }
    }
}
