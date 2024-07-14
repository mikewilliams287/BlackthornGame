using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPractice_2 : MonoBehaviour
{
    int myNumber;

    // Start is called before the first frame update
    void Start()
    {
        myNumber = -5;

        for (int i = -5; i <= 5; i += 1)
        {
            print(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
