using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    // [SerializeField] private TMP_Text healthDisplay;
    // [SerializeField] private GameObject healthBarGameObject;
    // [SerializeField] private DamageFlash _damageFlash;
    // [SerializeField] private GameObject boneSpawnerPrefab; // Assign BoneSpawner prefab in the inspector
    // [SerializeField] GameObject player;
    public int startingHealth = 20;
    // PlayerHealth health;
    private int currentHealth;
    // private HealthBar _healthbar;
    // //public static event Action OnPlayerDeath;
    // private bool isPlayerAlive = true; // Control player input based on this flag

    void Start()
    {
        print("object starting");
        // print("starting health" + startingHealth);
        // print("player health was " + currentHealth);
        // health = player.GetComponent<PlayerHealth>();
        // health.SetPlayerHealth(1000);
        // print("player health is now " + currentHealth);
        // healthDisplay.text = currentHealth.ToString();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        print("----------------------------------");
        print(currentHealth);
        // print("Player Health = " + currentHealth.ToString());
        // print("Damage Ammt = " + damageAmount.ToString());
        // currentHealth -= damageAmount;
        // healthDisplay.text = currentHealth.ToString();
        // print("Current Health = " + currentHealth.ToString());

        // //update health bar
        // _healthbar.UpdateHealthBar(startingHealth, currentHealth);

        // //damage flash shader effect
        // _damageFlash.CallDamageFlash();


        // if (currentHealth <= 0 && isPlayerAlive) //Ensure this block runs only once
        // {
        //     print("PLAYER DEAD");

        //     // Show the lose panel and final score
        //     //losePanel.SetActive(true);

        //     // Set the player as dead to prevent further input
        //     isPlayerAlive = false;

        //     //finalScoreText.text = scoreDisplayText.text;
        //     healthDisplay.text = "0";

        //     // Spawn BoneSpawner at player's last position
        //     Vector3 playerPosition = transform.position;
        //     Instantiate(boneSpawnerPrefab, playerPosition, Quaternion.identity);

        //     // Destroy player character
        //     Destroy(gameObject);

        // }
    }

    public void SetPlayerHealth(int banana)
    {
        currentHealth = banana;
    }

    public int ShowPlayerHealth()
    {
        return currentHealth;
    }
}
