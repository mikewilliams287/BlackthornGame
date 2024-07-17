using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalChallenges_2 : MonoBehaviour
{
    public int MyNumber1;
    public int MyNumber2;
    public int MyNumber3;
    public int MyNumber4;

    public string MyWord1;
    public string MyWord2;
    public string MyWord3;
    public string MyWord4;

    private string[] wordArray;
    private int[] allNumbers;


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
    void PrintReverse(string[] passedIn)
    {
        string[] newarray = new string[passedIn.Length];

        for (int i = 0; i < passedIn.Length; i++)
        {
            newarray[i] = passedIn[passedIn.Length - i - 1];
            print(newarray[i]);
        }

    }

    void MaxNumber(int[] passedInNumbers)
    {
        int highestnumber = 0;

        for (int i = 0; i < passedInNumbers.Length; i++)
        {
            if (passedInNumbers[i] > highestnumber)
            {
                highestnumber = passedInNumbers[i];
            }
        }
        print(highestnumber);


    }

    void IsUniform(string[] passedInArray)
    {
        bool status = true;
        int indexPosition = 0;

        // ["cat", "banana", "dog"]

        while (indexPosition < passedInArray.Length)
        {
            if (passedInArray[0] != passedInArray[indexPosition])
            {
                status = false;
            }

            indexPosition++;
        }
        print(status);
    }



    // Start is called before the first frame update
    void Start()
    {

        wordArray = new string[] { MyWord1, MyWord2, MyWord3, MyWord4 };
        allNumbers = new int[] { MyNumber1, MyNumber2, MyNumber3, MyNumber4 };



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print(SumArray(firstArray));
        }

        if (Input.GetKeyDown("2"))
        {
            PrintReverse(wordArray);
        }

        if (Input.GetKeyDown("3"))
        {
            MaxNumber(allNumbers);
        }

        if (Input.GetKeyDown("4"))
        {

            IsUniform(wordArray);
        }





    }
}



