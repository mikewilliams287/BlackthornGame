using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using System;

public class PlayerCtrl : MonoBehaviour
{
    // Player Model
    [SerializeField]
    private GameObject playerModel;

    // UI references
    public GameObject losePanel;
    public TMP_Text healthDisplay;
    public GameObject healthBarGameObject;
    private HealthBar _healthbar;
    private int totalHazardsDodged;
    public TMP_Text scoreDisplayText;
    public TMP_Text finalScoreText;

    // Movement Parameters
    public float playerSpeed;
    public int playerHealth;
    private int startingHealth;
    private float input;

    // Dash ability references & parameters
    public ParticleSystem dashParticleSystem;
    public float totalDashTime;
    private float dashTime;
    public float dashSpeed;
    private bool isDashing;

    // VFX references
    private DamageFlash _damageFlash;

    // Audio references
    private AudioSource source;
    public AudioClip[] allSFX;

    public GameObject boneSpawnerPrefab; // Assign BoneSpawner prefab in the inspector
    public static event Action OnPlayerDeath;
    private bool isPlayerAlive = true; // Control player input based on this flag

    Rigidbody2D rigidBody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (playerModel != null)
        {
            animator = playerModel.GetComponent<Animator>();
            if (animator == null) ;
            {
                Debug.LogError("Amimator component not found on Player Model");
            }
        }
        else
        {
            Debug.LogError("playerModel reference is missing");
        }

        rigidBody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        startingHealth = playerHealth;
        healthDisplay.text = playerHealth.ToString();

        _healthbar = healthBarGameObject.GetComponent<HealthBar>();

        _damageFlash = GetComponent<DamageFlash>();

        totalHazardsDodged = 0;
        scoreDisplayText.text = totalHazardsDodged.ToString();

        // Check if the "LosePanel" is active, if it is, disable it
        if (losePanel.activeSelf)
        {
            losePanel.SetActive(false);
        }


        //find the component and the game object via code, not the inspector
        //_healthbar = GameObject.Find("Canvas/HealthBarImage").GetComponent<HealthBar>();
    }

    private void Update()
    {
        if (!isPlayerAlive) return; // Skip Update if player is dead

        if (input != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false && input != 0)
        {
            dashTime = totalDashTime;
            playerSpeed += dashSpeed;
            isDashing = true;
            CreateDashParticle();
        }

        if (dashTime <= 0 && isDashing == true)
        {
            playerSpeed -= dashSpeed;
            isDashing = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPlayerAlive) return; // Skip FixedUpdate if player is dead

        input = Input.GetAxisRaw("Horizontal");
        //print(input);

        rigidBody.velocity = new Vector2(input * playerSpeed, rigidBody.velocity.y);

    }

    public void TakeDamage(int damageAmount)
    {
        //int randomSound = Random.Range(0, allSFX.Length);
        //source.clip = allSFX[randomSound];
        //source.Play();
        playerHealth -= damageAmount;
        healthDisplay.text = playerHealth.ToString();
        print("Current Health = " + playerHealth.ToString());

        //update health bar
        _healthbar.UpdateHealthBar(startingHealth, playerHealth);

        //damage flash shader effect
        _damageFlash.CallDamageFlash();


        if (playerHealth <= 0 && isPlayerAlive) //Ensure this block runs only once
        {
            print("PLAYER DEAD");

            // Show the lose panel and final score
            //losePanel.SetActive(true);

            // Set the player as dead to prevent further input
            isPlayerAlive = false;

            finalScoreText.text = scoreDisplayText.text;
            healthDisplay.text = "0";

            // Spawn BoneSpawner at player's last position
            Vector3 playerPosition = transform.position;
            Instantiate(boneSpawnerPrefab, playerPosition, Quaternion.identity);


            // Stop music if AudioManager instance exisits
            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopMusic();
            }

            // Destroy player character
            Destroy(gameObject);

        }
    }

    public void TrackScore()
    {
        totalHazardsDodged += 1;
        scoreDisplayText.text = totalHazardsDodged.ToString();
    }

    public void RestartGame()
    {

        print("RESTART GAME");
        // Get the active scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Reload the active scene
        SceneManager.LoadScene(currentScene.name);

    }

    public void CreateDashParticle()
    {
        dashParticleSystem.Play();
    }


    public void OnDestroy()
    {
        if (!isPlayerAlive)
        {
            OnPlayerDeath?.Invoke();
        }
    }
}
