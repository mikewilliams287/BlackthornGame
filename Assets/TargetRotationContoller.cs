using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TargetRotationContoller : MonoBehaviour
{

    [SerializeField]
    private Transform targetRotator; // Assign the TargetRotator GameObject in the Inspector


    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f; // Speed at which the rotation moves to min/max
    [SerializeField] private float returnSpeed = 100f; // Speed at which to return to the original position
    [SerializeField] private float minRotation = -45f; // Minimum rotation angle on z-axis
    [SerializeField] private float maxRotation = 45f; // Maximum rotation angle on the z-axis
    [SerializeField] private float snapSmoothTime = 0.15f;
    [SerializeField] private float returnSmoothTime = 0.2f;

    private float targetZRotation; // The z rotation value that the TargetRotator is moving towards
    private float originalZRotation; // The initial z rotation value of the TargetRotator
    private float currentVelocityZ = 0f;
    private bool returningToOriginal = false;

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

        // Handle input only on key down
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetZRotation = minRotation;
            returningToOriginal = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetZRotation = maxRotation;
            returningToOriginal = true;
        }

        float currentZRotation = GetNormalizedZRotation(targetRotator.localEulerAngles.z);

        float speed = returningToOriginal ? rotationSpeed : returnSpeed;
        //float smoothTime = (targetZRotation == originalZRotation) ? returnSmoothTime : snapSmoothTime;

        // SmoothDamp for easing in both directions
        //float newZRotation = Mathf.SmoothDamp(currentZRotation, targetZRotation, ref currentVelocityZ, smoothTime);

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
                targetZRotation = originalZRotation;
                returningToOriginal = false; // Only let it auto return once
                //currentVelocityZ = 0f; // reset easing velocity
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
