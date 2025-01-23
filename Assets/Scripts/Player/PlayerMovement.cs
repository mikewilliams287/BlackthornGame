using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
    private bool wasGrounded = false;

    Animator animator;
    Rigidbody2D rigidBody;
    //ParticleSystem walkParticles;

    void Start()
    {
        if (playerModel != null)
        {
            animator = playerModel.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Amimator component not found on Player Model");
            }
        }
        else
        {
            Debug.LogError("playerModel reference is missing");
        }

        rigidBody = GetComponent<Rigidbody2D>();
        //walkParticles = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (!wasGrounded && isGrounded())
        {
            TriggerLandingParticles();
        }


        if (isGrounded())
        {

            rigidBody.velocity = new Vector2(horizonalValue * moveSpeed, rigidBody.velocity.y);

            if (horizonalValue != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);

                if (walkParticles.isPlaying)
                {
                    walkParticles.Stop();
                    //Debug.Log("Particles are NOT playing");
                }
            }

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

    }



    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.5f, 0.25f), CapsuleDirection2D.Horizontal, 0, groundLayer);
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
        }
    }

    private void TriggerLandingParticles()
    {
        if (landingParticles != null)
        {
            landingParticles.Play(); // Triggers a burst if the particle system is set up correctly
        }
    }


    #endregion

}
