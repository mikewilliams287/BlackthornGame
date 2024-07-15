using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPractice_2 : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //Print every integer between -5 and 5
        /*
        for (int i = -5; i <= 5; i += 1)
        {
            print(i);
        }
        */

        //print all even numbers between 10 and 50
        /*
        for (int i = 10; i <= 50; i += 1)
        {
            if (i % 2 == 0)
            {
                print(i);
            }
        }
        */

        //Print numbers between 1 and 100 that are divisiable by 5 AND 3

        for (int i = 1; i <= 100; i += 1)
        {
            if (i % 5 == 0 && i % 3 == 0)
            {
                print(i + " / 5 is " + (i / 5) + " and " + i + " / 3 is " + (i / 3));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
