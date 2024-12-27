using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTest : MonoBehaviour
{
    PlayerHealthTest playerHealthTest;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthTest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthTest>();


    }

    // Update is called once per frame
    void Update()
    {
        playerHealthTest.SayBanana();
    }
}
