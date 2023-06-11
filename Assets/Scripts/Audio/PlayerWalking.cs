using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalking : MonoBehaviour
{
    #region REFERENCES
    public AudioSource playerFootsteps;
    public AudioSource playerJumping;
    public AudioSource playerLanding;
    public PlayerMovement playerMovement;

    #endregion

    #region INPUT

    private bool hasJumped = false;
    private bool hasLanded = false;
    private Vector2 _moveInput;
    private float landingSoundDelay = 0.5f;
    #endregion
    private void Start()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        #region SOUND_ENABLE/DISABLE
        _moveInput = playerMovement.MoveInput;
        if (_moveInput.x != 0 && playerMovement.GetIsGrounded)
        {
            playerFootsteps.enabled = true;
        }
        else
        {
            playerFootsteps.enabled = false;
        }
        
        

        if (playerMovement.GetIsJumping && !playerMovement.GetIsGrounded)
        {
           hasJumped = true;
        }

        if (playerMovement.GetIsGrounded && hasJumped)
        {
            // Play landing sound here
            hasJumped = false;
            StartCoroutine(PlayLandingSound());
        }

        #endregion
    }
    private IEnumerator PlayLandingSound()
    {
        playerLanding.enabled = true;
        // Play landing sound here

        yield return new WaitForSeconds(landingSoundDelay);

        playerLanding.enabled = false;
    }


}
