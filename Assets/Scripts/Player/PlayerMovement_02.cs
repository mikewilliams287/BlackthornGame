using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_02 : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] GameObject playerModel;

    [Header("Player Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    private float horizontalValue;
    private float moveInput;
    private bool jumpJustPressed;
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


    private void Move()
    {
        //if (isGrounded())
        //{
        moveInput = UserInput.instance.MoveInput.x;

        if (moveInput > 0 || moveInput < 0)
        {
            Debug.Log("MOVEMENT INPUT");
            animator.SetBool("isRunning", true);
            TurnCheck();
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);
        //}
        //else
        //{
        //    return;
        //}
    }

    private void TurnCheck()
    {
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.5f, 0.25f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void Jump()
    {
        jumpJustPressed = UserInput.instance.JumpJustPressed;

        if (jumpJustPressed == true)
        {
            Debug.Log("JUMPING");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
        }
    }
}





