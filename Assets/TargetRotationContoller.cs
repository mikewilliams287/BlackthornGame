using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class TargetRotationContoller : MonoBehaviour
{

    [SerializeField]
    private Transform targetRotator; // Assign the TargetRotator GameObject in the Inspector


    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f; // Speed at which the rotation moves to min/max
    [SerializeField] private float returnSpeed = 100f; // Speed at which to return to the original position
    [SerializeField] private float minRotation = -45f; // Minimum rotation angle on z-axis
    [SerializeField] private float maxRotation = 45f; // Maximum rotation angle on the z-axis
    //[SerializeField] private float snapSmoothTime = 0.15f;


    [Header("Overshoot Settings")]
    [SerializeField] private float overshootAmplitude = 10f; // degrees
    [SerializeField] private float overshootFrequency = 3f;  // Hz
    [SerializeField] private float overshootDamping = 2f;    // Damping factor

    [Header("Input Delay Settings")]
    [SerializeField] private float inputDelay = 0.2f; // seconds

    private float targetZRotation; // The z rotation value that the TargetRotator is moving towards
    private float originalZRotation; // The initial z rotation value of the TargetRotator
    //private float currentVelocityZ = 0f;
    private bool returningToOriginal = false;
    private bool isOscillating = false;
    private float oscillationTimer = 0f;
    private float oscillationStartZ = 0f;
    private bool canAcceptInput = true;
    private float inputDelayTimer = 0f;

    private void Start()
    {
        // Store the initial z rotation value
        originalZRotation = GetNormalizedZRotation(targetRotator.localEulerAngles.z);
        //if (originalZRotation > 180) originalZRotation -= 360; //Normalize to -180 to 180 range

        // Set the inital target rotation to the current Z rotation
        targetZRotation = originalZRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRotator == null) return;

        // Update input delay timer
        if (inputDelayTimer > 0f)
        {
            inputDelayTimer -= Time.deltaTime;
            if (inputDelayTimer <= 0f)
            {
                canAcceptInput = true;
            }
        }

        float currentZRotation = GetNormalizedZRotation(targetRotator.localEulerAngles.z);
        float speed = returningToOriginal ? rotationSpeed : returnSpeed;

        if (isOscillating)
        {
            oscillationTimer += Time.deltaTime;
            float dampedAmplitude = overshootAmplitude * Mathf.Exp(-overshootDamping * oscillationTimer);
            float oscillation = dampedAmplitude * Mathf.Sin(2 * Mathf.PI * overshootFrequency * oscillationTimer);
            float oscillationZRotation = originalZRotation + oscillation;

            // Smoothly move toward the oscillation value using returnSpeed
            float smoothZRotation = Mathf.MoveTowards(currentZRotation, oscillationZRotation, returnSpeed * Time.deltaTime);
            targetRotator.localEulerAngles = new Vector3(
                targetRotator.localEulerAngles.x,
                targetRotator.localEulerAngles.y,
                smoothZRotation
            );
            if (Mathf.Abs(dampedAmplitude) < 0.1f)
            {
                isOscillating = false;
                targetRotator.localEulerAngles = new Vector3(
                    targetRotator.localEulerAngles.x,
                    targetRotator.localEulerAngles.y,
                    originalZRotation
                );
            }
            return;
        }

        // Move toward the target rotation
        float newZRotation = Mathf.MoveTowards(currentZRotation, targetZRotation, speed * Time.deltaTime);
        targetRotator.localEulerAngles = new Vector3(
            targetRotator.localEulerAngles.x,
            targetRotator.localEulerAngles.y,
            newZRotation
        );

        // Start returning to original after reaching target
        if (returningToOriginal && Mathf.Approximately(newZRotation, targetZRotation))
        {
            if (targetZRotation != originalZRotation)
            {
                // Start oscillation
                isOscillating = true;
                oscillationTimer = 0f;
                oscillationStartZ = newZRotation;
                targetZRotation = originalZRotation;
                returningToOriginal = false;
                // canAcceptInput will be set in oscillation block
            }
        }
    }

    // Input System method for rotating head (single function)
    public void RotateHead(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed && canAcceptInput && inputDelayTimer <= 0f)
        {
            isOscillating = false;

            float direction = context.ReadValue<float>();
            if (direction < 0)
            {
                targetZRotation = minRotation;
                returningToOriginal = true;
                canAcceptInput = false;
                inputDelayTimer = inputDelay;
                Debug.Log("Left Arrow Pressed (Input System)");
            }
            else if (direction > 0)
            {
                targetZRotation = maxRotation;
                returningToOriginal = true;
                canAcceptInput = false;
                inputDelayTimer = inputDelay;
                Debug.Log("Right Arrow Pressed (Input System)");
            }
        }
    }

    // Helper method to normalize Z rotation to the range -180 to 180
    private float GetNormalizedZRotation(float zRotation)
    {
        if (zRotation > 180) zRotation -= 360;
        return zRotation;
    }
}
