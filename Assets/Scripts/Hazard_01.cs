using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_01 : MonoBehaviour
{

    public int minSpeed;
    public int maxSpeed;
    public int hazardDamage;

    public GameObject explosion;
    int hazardSpeed;

    PlayerCtrl playerScript;


    // Start is called before the first frame update
    void Start()
    {
        hazardSpeed = Random.Range(minSpeed, maxSpeed);
        print(hazardSpeed);

        //set playerScript to be the script on the player game object, so that this script has access to its functions
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();


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
            playerScript.TakeDamage(hazardDamage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (hitObject.tag == "Ground")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
