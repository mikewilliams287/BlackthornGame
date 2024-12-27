using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impactHazard_fire : MonoBehaviour
{

    [SerializeField] float killDelay = 10f;
    public int fireDamageAmount;
    [SerializeField] float damageInterval = 1f; //Time interval between damage applications

    private Coroutine damageCoroutine; // Keeps track of the active Coroutine

    void Start()
    {
        // Destroy the impact hazard after a delay
        Destroy(gameObject, killDelay);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colloded object has the PlayerHealth_02 component
        PlayerHealth_02 playerHealth = other.GetComponent<PlayerHealth_02>();

        if (playerHealth != null && damageCoroutine == null)
        {
            print("ENTERED FIRE");
            // Start the Coroutine to apply damage repeatedly
            damageCoroutine = StartCoroutine(ApplyFireDamage(playerHealth));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        // Stop the Coroutine when the player leaves the fire hazard
        PlayerHealth_02 playerHealth = other.GetComponent<PlayerHealth_02>();
        if (playerHealth != null && damageCoroutine != null)
        {
            print("EXITED FIRE");
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator ApplyFireDamage(PlayerHealth_02 playerHealth)
    {
        while (true)
        {
            playerHealth.TakeDamage(fireDamageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }

}
