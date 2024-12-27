using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengePractice : MonoBehaviour
{
    //SumArray()
    //Returns the sum of all the items in an array

    int[] numberArray = new int[] { 1, 2, 3, 4, 5 };

    int sumArray()
    {
        int result = 0;
        for (int i = 0; i < numberArray.Length; i++)
        {
            result = result + numberArray[i];
            print(result);
        }
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("The final answer is " + sumArray());
        }


    }
}
