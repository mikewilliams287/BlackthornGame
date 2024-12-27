using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FunctionsPractice_2 : MonoBehaviour
{
    public int myNumber1;
    public int myNumber2;
    public string TextToRepeat;

    //Return true if number is even, false if odd
    bool IsEven()
    {
        if (myNumber1 % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Return largest number out of the two numbers
    void WhatIsBigger(int number1, int number2)
    {
        if (number1 - number2 > 0)
        {
            print(number1 + " is larger than " + number2);
        }
        else
        {
            print(number2 + " is larger than " + number1);
        }
    }

    //Print out some text as many times as the number provided
    void Repeat(string text, int times)
    {
        for (int i = 1; i <= times; i += 1)
        {
            print(text);
        }


    }

    //Return the factorial of a number
    int Factorial(int number)
    {
        int FactorialResult = number;
        for (int i = number - 1; i > 1; i -= 1)
        {
            FactorialResult *= (i);
        }
        return FactorialResult;

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print(IsEven());
        }

        if (Input.GetKeyDown("2"))
        {
            WhatIsBigger(myNumber1, myNumber2);
        }

        if (Input.GetKeyDown("3"))
        {
            Repeat(TextToRepeat, myNumber1);
        }

        if (Input.GetKeyDown("4"))
        {
            print(Factorial(myNumber1));
        }
    }
}
