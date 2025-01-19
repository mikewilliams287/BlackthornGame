using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Hazard_01 : MonoBehaviour
{

    public int minSpeed;
    public int maxSpeed;
    public int hazardDamageAmmount = 2;
    public float particleKillDelay = 10f;

    public float impactHazardOffset;

    public GameObject explosion;

    //[SerializeField] GameObject collectable;
    [SerializeField] private GameObject[] collectablesToSpawn;
    [SerializeField] GameObject impactHazard;

    public GameObject smokevfx;
    int hazardSpeed;

    PlayerHealth_02 playerHealth;


    static private string PLAYER_TAG = "Player";
    static private string COLLIDER_TAG = "Ground";


    // Start is called before the first frame update
    void Start()
    {
        hazardSpeed = Random.Range(minSpeed, maxSpeed);


        // Find the GameObject via the tag, and then find the script component on that object
        //playerHealth = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerHealth_02>();


    }

    // Update is called once per frame
    void Update()
    {
        // Move the hazard according to the hazard speed
        transform.Translate(Vector2.down * hazardSpeed * Time.deltaTime);

    }


    //Listen for a collision with another collision object, then run the script on the player game object to apply damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == COLLIDER_TAG)
        {
            print("GROUND HIT");

            // Get the collision contact point
            ContactPoint2D contact = collision.contacts[0];

            // Calculate the position for the impactHazard
            Vector3 spawnPostion = new Vector3(contact.point.x, contact.point.y - (impactHazard.transform.localScale.y / 2), 0);

            //Instantiate the impactHazard at the calculated position
            Instantiate(impactHazard, spawnPostion, Quaternion.identity);

            //Instantiate the collectable

            int collectableIndex = Random.Range(0, collectablesToSpawn.Length);
            GameObject collectableToSpawn = collectablesToSpawn[collectableIndex];
            Instantiate(collectableToSpawn, spawnPostion, Quaternion.identity);

            // Instantiate explosion VFX
            Instantiate(explosion, transform.position, Quaternion.identity);

            HandleSmoke();

            Destroy(gameObject);
        }
        else if (collision.collider.tag == PLAYER_TAG)
        {
            print("PLAYER HIT");

            // Find the GameObject via the tag, and then find the script component on that object
            playerHealth = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerHealth_02>();

            // Call the "TakeDamage" function on the PlayerHealth script, and pass it the damage ammount
            playerHealth.TakeDamage(hazardDamageAmmount);

            Instantiate(explosion, transform.position, Quaternion.identity);
            HandleSmoke();
            Destroy(gameObject);
        }
    }


    // void OnTriggerEnter2D(Collider2D hitObject)
    // {
    //     if (hitObject.tag == "Player")
    //     {
    //         print("PLAYER HIT");
    //         playerScript.TakeDamage(hazardDamage);
    //         Instantiate(explosion, transform.position, Quaternion.identity);
    //         HandleSmoke();
    //         Destroy(gameObject);
    //     }

    //     else if (hitObject.tag == "Ground")
    //     {
    //         print("GROUND HIT");
    //         playerScript.TrackScore();
    //         HandleSmoke();
    //         Destroy(gameObject);
    //         Instantiate(explosion, transform.position - , Quaternion.identity);
    //         Instantiate(impactHazard, transform.position, Quaternion.identity);

    //     }
    // }


    //Un-parent the VFX game object, stop it from emitting, and destroy after delay

    void HandleSmoke()
    {
        //Transform smokeTransform = transform.Find("Smoke");

        if (smokevfx != null)
        {
            //Un-parent the smoke object
            smokevfx.transform.parent = null;

            //Stop the particles from spawning
            var visualEffect = smokevfx.GetComponent<VisualEffect>();
            if (visualEffect != null)
            {
                visualEffect.Stop();
                //print("VFX STOPPED");
            }

            //Destroy the smoke object
            Destroy(smokevfx.gameObject, particleKillDelay);
            //print("VFX DESTROYED");
        }


    }
}
