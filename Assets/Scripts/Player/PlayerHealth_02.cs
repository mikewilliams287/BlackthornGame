using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class PlayerHealth_02 : MonoBehaviour
{
    public int startingHealth = 20;
    private int currentHealth;
    public TMP_Text healthDisplay;
    public HealthBar _healthbar;
    private int totalHazardsDodged;
    // public TMP_Text scoreDisplayText;
    // public TMP_Text finalScoreText;
    public DamageFlash _damageFlash;

    private bool isPlayerAlive = true;

    public GameObject boneSpawnerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        print("FROM START OF PLAYERHEALTH OBJECT (separate from hazard): Current Health = " + currentHealth);

        healthDisplay.text = currentHealth.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintPlayerHealth()
    {
        print("The Current Player Health is " + currentHealth);
        // healthDisplay.text = currentHealth.ToString();
    }

    public void SetPlayerHealth(int health)
    {
        print("my current health is " + currentHealth);
        print("Setting Player Health to " + health);
        currentHealth = health;

    }

    public void TakeDamage(int damage)
    {
        print("currentHealth is: " + currentHealth);
        print("going to take damage: " + damage);
        currentHealth -= damage;
        print("health is now " + currentHealth);

        // Update health text
        healthDisplay.text = currentHealth.ToString();

        //update health bar
        _healthbar.UpdateHealthBar(startingHealth, currentHealth);

        //damage flash shader effect
        _damageFlash.CallDamageFlash();

        if (currentHealth <= 0 && isPlayerAlive) //Ensure this block runs only once
        {
            print("PLAYER DEAD");

            // Show the lose panel and final score
            //losePanel.SetActive(true);

            // Set the player as dead to prevent further input
            isPlayerAlive = false;

            //finalScoreText.text = scoreDisplayText.text;
            healthDisplay.text = "0";

            // Spawn BoneSpawner at player's last position
            Vector3 playerPosition = transform.position;
            Instantiate(boneSpawnerPrefab, playerPosition, Quaternion.identity);

            // Destroy player character
            Destroy(gameObject);
        }
    }
}

