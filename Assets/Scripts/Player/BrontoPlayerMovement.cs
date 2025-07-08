using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrontoPlayerMovement : MonoBehaviour
{
    [Header("Player Component References")]

    [SerializeField] GameObject playerModel;


    [Header("Player Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;


    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("VFX")]
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem landingParticles;

    private float horizonalValue;
    private bool wasGrounded;

    private bool hasJumped = false; // Track if the player has jumped at least once

    Animator animator;
    Rigidbody2D rigidBody;

    private bool warnedNoAnimator = false;
    private bool warnedNoWalkParticles = false;
    private bool warnedNoLandingParticles = false;

    [SerializeField, Tooltip("Displays if the player is grounded or jumping (Read Only)")]
    private string jumpStatus; // This will be visible in the inspector

    void Start()
    {
        if (playerModel != null)
        {
            animator = playerModel.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Amimator component not found on Player Model, animation will not play.");
            }
        }
        else
        {
            Debug.LogError("playerModel reference is missing");
        }

        rigidBody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        jumpStatus = isGrounded() ? "Grounded" : "Jumping";

    }

    private void FixedUpdate()
    {
        bool currentlyGrounded = isGrounded(); //Check once per frame

        if (!wasGrounded && currentlyGrounded && hasJumped) // Landed event (was in the air, is now grounded), only trigger if the player has jumped before
        {
            TriggerLandingParticles();
        }

        if (currentlyGrounded)
        {

            rigidBody.velocity = new Vector2(horizonalValue * moveSpeed, rigidBody.velocity.y);
            if (animator != null)
            {
                animator.SetBool("isRunning", horizonalValue != 0);
            }
            else if (!warnedNoAnimator)
            {
                Debug.LogWarning("Animator component is missing. Animation will not play.");
                warnedNoAnimator = true;
            }

            if (walkParticles != null)
            {
                if (horizonalValue != 0)
                {
                    if (!walkParticles.isPlaying) walkParticles.Play();
                }
                else
                {
                    if (walkParticles.isPlaying) walkParticles.Stop();
                }
            }
            else if (!warnedNoWalkParticles)
            {
                Debug.LogWarning("walkParticles is not assigned. Walking VFX will not play.");
                warnedNoWalkParticles = true;
            }

            // Flip the character ONLY when moving
            if (horizonalValue > 0)
            {
                transform.eulerAngles = Vector3.zero; // Face right

            }
            else if (horizonalValue < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // Face left

            }
        }
        else
        {
            if (walkParticles != null)
            {
                if (walkParticles.isPlaying) walkParticles.Stop();
            }
            else if (!warnedNoWalkParticles)
            {
                Debug.LogWarning("walkParticles is not assigned. Walking VFX will not play.");
                warnedNoWalkParticles = true;
            }
        }

        wasGrounded = currentlyGrounded; // Update at end
    }

    /*
    if (horizonalValue > 0)
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (horizonalValue < 0)
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }



    // Turn on particle system
    if (!walkParticles.isPlaying) // Play only if not already playing
    {
        walkParticles.Play();
        //Debug.Log("Particles are playing");
    }

}
else
{
    if (walkParticles.isPlaying)
    {
        walkParticles.Stop();
        //Debug.Log("Particles are NOT playing");
    }
    return;
}

// Store current grounded state for the next frame
wasGrounded = isGrounded();

}
*/



    private bool isGrounded()
    {

        bool grounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.5f, 0.25f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        //Debug.Log("Grounded: " + grounded);
        return grounded;
    }


    #region PLAYER_MOVEMENT
    public void Move(InputAction.CallbackContext context)
    {
        horizonalValue = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            Debug.Log("JUMPING");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
            hasJumped = true; // Mark that the player has jumped at least once
        }
    }

    private void TriggerLandingParticles()
    {
        if (landingParticles != null)
        {
            landingParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Ensure it fully stops
            landingParticles.Play(); // Triggers a burst if the particle system is set up correctly
            Debug.Log("Landing Particles triggered!");
        }
        else if (!warnedNoLandingParticles)
        {
            Debug.LogWarning("landingParticles is not assigned. Landing VFX will not play.");
            warnedNoLandingParticles = true;
        }
    }


    #endregion

}
