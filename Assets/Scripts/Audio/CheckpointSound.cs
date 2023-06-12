using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSound : MonoBehaviour
{
    #region REFERENCES
    public AudioSource checkPointActive;
    private float _checkpointSoundDelay = 2.0f;
    public Checkpoint checkpoint;
    private bool _alreadyPlayed;
   
    #endregion
    private void Start()
    {
        Checkpoint checkpoint = GetComponent<Checkpoint>();
    }

    
    void Update()
    {
        if(_alreadyPlayed)
        {
            return;
        }
        if(checkpoint.GetIsEnabled && !_alreadyPlayed)
        {
            StartCoroutine(PlayCheckpointSound());
        }
        
    }

    private IEnumerator PlayCheckpointSound()
    {
        checkPointActive.enabled = true;
        // Play landing sound here

        yield return new WaitForSeconds(_checkpointSoundDelay);
        _alreadyPlayed = true;
        checkPointActive.enabled = false;
    }
}
