using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalChallenges_2 : MonoBehaviour
{
    public int MyNumber1;
    public int MyNumber2;
    public int MyNumber3;

    public string MyWord1;
    public string MyWord2;
    public string MyWord3;
    public string MyWord4;



    int[] firstArray = new[] { 5, 10, 15 };
    //string[] wordArray = new string[];

    //SumArray()
    //Return the sum of all the items in the array
    int SumArray(int[] array1)
    {
        int result = 0;
        for (int i = 0; i < array1.Length; i++)
        {
            result += array1[i];
        }
        return result;
    }

    //PrintReverse()
    //Prints all the items in an array in reverse order
    void PrintReverse(string[] array1)
    {
        string[] newArray = new string[array1.Length];
        for (int i = array1.Length - 1; i >= 0; i--)
        {
            print(array1[i]);
            newArray[array1.Length - i - 1] = array1[i];
        }
        // print(newArray[0] + newArray[1] + newArray[2] + newArray[3]);
    }


    // Start is called before the first frame update
    void Start()
    {
        string[] wordArray = new[] { MyWord1, MyWord2, MyWord3, MyWord4 };

        print(wordArray[0] + wordArray[1]);

        PrintReverse(wordArray);




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print(SumArray(firstArray));
        }





    }
}

