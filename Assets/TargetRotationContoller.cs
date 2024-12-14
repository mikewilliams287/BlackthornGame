using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TargetRotationContoller : MonoBehaviour
{

    [SerializeField]
    private Transform targetRotator; // Assign the TargetRotator GameObject in the Inspector

    [SerializeField]
    private float rotationSpeed = 100f; // Speed at which the rotation moves to min/max

    [SerializeField]
    private float returnSpeed = 100f; // Speed at which to return to the original position

    [SerializeField]
    private float minRotation = -45f; // Minimum rotation angle on z-axis

    [SerializeField]
    private float maxRotation = 45f; // Maximum rotation angle on the z-axis

    private float targetZRotation; // The z rotation value that the TargetRotator is moving towards

    private float originalZRotation; // The initial z rotation value of the TargetRotator

    private void Start()
    {
        // Store the initial z rotation value
        originalZRotation = targetRotator.localEulerAngles.z;
        if (originalZRotation > 180) originalZRotation -= 360; //Normalize to -180 to 180 range

        // Set the inital target rotation to the current Z rotation
        targetZRotation = GetNormalizedZRotation(targetRotator.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRotator == null) return;

        // Get horizontal input (-1 for left, +1 for right)
        float horizontalInput = Input.GetAxis("Horizontal") * -1;

        if (horizontalInput < 0) // Left arrow key pressed
        {
            targetZRotation = minRotation;

            //Calculate the new z-axis rotation step
            //float rotationStep = horizontalInput * rotationSpeed * Time.deltaTime;

            // Get the current z-axis rotation, accounting for Unity's 0-360 wrap-around
            //float currentZRotation = targetRotator.localEulerAngles.z;
            //if (currentZRotation > 180) currentZRotation -= 360; // Convert to a -180 to 180 range

            // Compute the new z rotation, clamped to the min and max values
            //float newZRRotation = Mathf.Clamp(currentZRotation + rotationStep, minRotation, maxRotation);

            //Apply the new rotation, preserving the X and Y axis values;
            //targetRotator.localEulerAngles = new Vector3(targetRotator.localEulerAngles.x, targetRotator.localEulerAngles.y, newZRRotation);
        }
        else if (horizontalInput > 0) // Right arrow key pressed
        {
            targetZRotation = maxRotation;
        }
        else
        {
            targetZRotation = originalZRotation;

            // Smoothly return to the original position when no input is detected
            //float currentZRotation = targetRotator.localEulerAngles.z;
            //if (currentZRotation > 180) currentZRotation -= 360; // Normalize to -180 to 180 range

            //float returnZRotation = Mathf.Lerp(currentZRotation, originalZRotation, returnSpeed * Time.deltaTime);

            //targetRotator.localEulerAngles = new Vector3(targetRotator.localEulerAngles.x, targetRotator.localEulerAngles.y, returnZRotation);
        }

        // Smoothly move toward the target rotation
        float currentZRotation = GetNormalizedZRotation(targetRotator.localEulerAngles.z);
        float newZRotation = Mathf.MoveTowards(currentZRotation, targetZRotation, rotationSpeed * Time.deltaTime);

        // Apply the new Z rotation
        targetRotator.localEulerAngles = new Vector3(targetRotator.localEulerAngles.x, targetRotator.localEulerAngles.y, newZRotation);

    }

    // Helper method to normalize Z rotation to the range -180 to 180
    private float GetNormalizedZRotation(float zRotation)
    {
        if (zRotation > 180) zRotation -= 360;
        return zRotation;
    }
}
