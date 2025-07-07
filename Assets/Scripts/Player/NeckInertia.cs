using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckInertia : MonoBehaviour
{
    public Rigidbody2D[] neckBones;
    public float inertiaForceMultiplier = 1.5f;

    private Vector2 lastVelocity;

    void FixedUpdate()
    {
        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        Vector2 acceleration = (currentVelocity - lastVelocity) / Time.fixedDeltaTime;

        foreach (var bone in neckBones)
        {
            bone.AddForce(-acceleration * inertiaForceMultiplier, ForceMode2D.Force);
        }

        lastVelocity = currentVelocity;
    }
}
