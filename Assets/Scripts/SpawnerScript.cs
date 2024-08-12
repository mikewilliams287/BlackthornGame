using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnerScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] hazards;
    private float timeBtwnSpawns;
    public float spawnRate;

    public TMP_Text scoreDisplay;

    public float minTimeBtwnSpawns;
    public float decreaseAmnt;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        print("we started");
        if (player == null)
        {
            EditorApplication.isPlaying = false;
            print("player gameobject not defined!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (timeBtwnSpawns <= 0)
            {
                print("hello");
                Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject selectedHazard = hazards[Random.Range(0, hazards.Length)];

                Instantiate(selectedHazard, selectedSpawnPoint.position, Quaternion.identity);

                if (spawnRate > minTimeBtwnSpawns)
                {
                    spawnRate -= decreaseAmnt;
                }

                timeBtwnSpawns = spawnRate;

            }
            else
            {
                timeBtwnSpawns -= Time.deltaTime;
            }
        }
        else
        {
            print("player gameobject not defined!");
        }
    }
}
