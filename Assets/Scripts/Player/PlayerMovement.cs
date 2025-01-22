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

    private float horizonalValue;

    Animator animator;
    Rigidbody2D rigidBody;

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
    }

    private void FixedUpdate()
    {


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
            }

            if (horizonalValue > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (horizonalValue < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
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


    #endregion

}
