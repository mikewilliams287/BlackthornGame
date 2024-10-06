using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Hazard_01 : MonoBehaviour
{

    public int minSpeed;
    public int maxSpeed;
    public int hazardDamage;
    public float particleKillDelay = 10f;

    public GameObject explosion;

    public GameObject smokevfx;
    int hazardSpeed;

    PlayerCtrl playerScript;


    // Start is called before the first frame update
    void Start()
    {
        hazardSpeed = Random.Range(minSpeed, maxSpeed);
        //print(hazardSpeed);

        //set playerScript to be the script on the player game object, so that this script has access to its functions
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

        //


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * hazardSpeed * Time.deltaTime);

    }

    //Listen for a collision with another collision object, then run the script on the player game object to apply damage
    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Player")
        {
            print("PLAYER HIT");
            playerScript.TakeDamage(hazardDamage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            HandleSmoke();
            Destroy(gameObject);
        }

        else if (hitObject.tag == "Ground")
        {
            print("GROUND HIT");
            playerScript.TrackScore();
            HandleSmoke();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);

        }
    }

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
                print("VFX STOPPED");
            }

            //Destroy the smoke object
            Destroy(smokevfx.gameObject, particleKillDelay);
            print("VFX DESTROYED");
        }


    }
}
