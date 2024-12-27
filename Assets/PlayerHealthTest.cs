using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public int startingHealth = 20;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SayBanana()
    {
        print("BANANA");
    }
}
