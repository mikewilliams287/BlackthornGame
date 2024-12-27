using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayPractice : MonoBehaviour
{
    int[] myArray = new int[3];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < myArray.Length; i++)
        {
            myArray[i] = Random.Range(0, 100);
            //print(myArray[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                print("index " + i + " value is " + myArray[i]);

            }

            //print(myArray[0]);
            //print(myArray[myArray.Length]);
        }
    }
}
