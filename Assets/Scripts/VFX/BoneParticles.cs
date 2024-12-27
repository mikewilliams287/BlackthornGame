using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneParticles : MonoBehaviour
{
    public List<GameObject> bonePrefabs; // Array of bone prefabs with Rigidbody2d and Collider2d
    public ParticleSystem boneParticleSystem;
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100]; //  creates a private list to store particle data from the Particle System
    private int lastParticleCount = 0; // Track the last particle count to detect new particles

    private bool hasRunAlready = false;

    // Update is called once per frame
    void Update()
    {
        int numParticlesAlive = boneParticleSystem.GetParticles(particles);
        print("number of particles is" + numParticlesAlive);

        // Loop only through newly emitted particles
        if (!hasRunAlready)
        {
            print("RUNNING FOR LOOP");
            for (int i = lastParticleCount; i < numParticlesAlive; i++)
            {
                Vector3 particlePosition = particles[i].position; // Offset by system position

                // Select a random bone prefab
                GameObject randomBonePrefab = bonePrefabs[Random.Range(0, bonePrefabs.Count)];

                // Convert particle rotation from radians (2D) to degrees
                float particleRotationZ = particles[i].rotation * Mathf.Rad2Deg; //Z-axis rotation in degrees
                Quaternion boneRotation = Quaternion.Euler(0, 0, particleRotationZ); // 2D rotation around z-axis

                // Instantiate the bone prefab with the particle's rotation
                GameObject bone = Instantiate(randomBonePrefab, particlePosition, boneRotation);
                print("prefab" + i + "instantiated");

                //Apply velocity from particle to the Rigidbody2D
                Rigidbody2D rb = bone.GetComponent<Rigidbody2D>();
                rb.velocity = particles[i].velocity;
                hasRunAlready = true;
            }
        }

        // print("last particle count before is: " + lastParticleCount);
        // Update last particle count for next frame
        // lastParticleCount = numParticlesAlive;
        // print("last particle count after is: " + lastParticleCount);

        // Clear the particle system after spawning objects
        //boneParticleSystem.Clear();
        // print("particle system cleared");

    }
}
