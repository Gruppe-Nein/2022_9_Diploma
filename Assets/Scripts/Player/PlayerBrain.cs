using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    public PlayerData Data;
    public StateMachine movementSM;
    /*public StandingState standing;
    public DuckingState ducking;
    public JumpingState jumping;*/

    // Start is called before the first frame update
    void Start()
    {
        movementSM = new StateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
