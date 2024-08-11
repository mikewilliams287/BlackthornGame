using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject losePanel;
    public TMP_Text healthDisplay;
    public float playerSpeed;
    public int playerHealth;
    private int startingHealth;

    public GameObject healthBarGameObject;

    private HealthBar _healthbar;

    private float input;

    public ParticleSystem dashParticleSystem;
    public float totalDashTime;
    private float dashTime;
    public float dashSpeed;
    private bool isDashing;

    private AudioSource source;
    public AudioClip[] allSFX;
    Rigidbody2D rb;
    Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        startingHealth = playerHealth;
        healthDisplay.text = playerHealth.ToString();

        _healthbar = healthBarGameObject.GetComponent<HealthBar>();


        //find the component and the game object via code, not the inspector
        //_healthbar = GameObject.Find("Canvas/HealthBarImage").GetComponent<HealthBar>();
    }

    private void Update()
    {
        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
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
        input = Input.GetAxisRaw("Horizontal");
        //print(input);

        rb.velocity = new Vector2(input * playerSpeed, rb.velocity.y);

    }

    public void TakeDamage(int damageAmount)
    {
        int randomSound = Random.Range(0, allSFX.Length);
        source.clip = allSFX[randomSound];
        source.Play();
        playerHealth -= damageAmount;
        healthDisplay.text = playerHealth.ToString();

        //update health bar
        _healthbar.UpdateHealthBar(startingHealth, playerHealth);



        if (playerHealth <= 0)
        {
            print("PLAYER DEAD");
            losePanel.SetActive(true);
            Destroy(gameObject);
            healthDisplay.text = "0";

            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopMusic();
            }

        }
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
}
