using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnInArea : MonoBehaviour
{

    public GameObject player;
    //public TMP_Text scoreDisplayText;
    private BoxCollider2D spawnArea;
    [SerializeField] private GameObject[] prefabsToSpawn;

    [SerializeField] private float spawnRate;

    private float timeBtwnSpawns;

    // private int totalHazardsDodged;
    [SerializeField] private float minTimeBtwnSpawns;

    [SerializeField] private float decreaseAmmount;

    [SerializeField] private float maxHazardRotation;
    [SerializeField] private float minHazardRotation;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        //SpawnHazard();

        //totalHazardsDodged = 0;
        //scoreDisplayText.text = totalHazardsDodged.ToString();

    }


    // Method to get a random point within the bounds of the Box Collider 2D
    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

    void SpawnHazard()
    {
        // Choose a random hazard from the array
        int hazardIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject hazardToSpawn = prefabsToSpawn[hazardIndex];

        // Get a random position within the Box Collider's bounds
        Vector2 randomSpawnPosition = GetRandomPointInBounds(spawnArea.bounds);

        // Get a random position between the min and max
        float randomRotation = Random.Range(minHazardRotation, maxHazardRotation);

        Instantiate(hazardToSpawn, randomSpawnPosition, Quaternion.Euler(0, 0, randomRotation));


        //print("total hazards dodged = " + totalHazardsDodged);
    }



    // Update is called once per frame
    void Update()
    {
        //if (player != null)
        //{
        if (timeBtwnSpawns <= 0)
        {
            SpawnHazard();

            if (spawnRate > minTimeBtwnSpawns)
            {
                spawnRate -= decreaseAmmount;
            }

            timeBtwnSpawns = spawnRate;
            //print("time between spawns = " + timeBtwnSpawns);

        }
        else
        {
            timeBtwnSpawns -= Time.deltaTime;
        }
        //}
        //else
        //{
        //print("player gameobject not defined!");
        //}

    }
}
