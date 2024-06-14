using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FunctionsPractice : MonoBehaviour
{

    public int myNumber1;
    public int myNumber2;
    public string myWord;

    // Returns true if number is even and false if number is odd
    void IsEven()
    {
        if (myNumber1 % 2 == 0)
        {
            print(myNumber1 + " is and EVEN number.");
        }
        else
        {
            print(myNumber1 + " is an ODD number");
        }
    }
    // Returns the biggest number out of the two:
    int WhatisBigger(int number1, int number2)
    {
        int difference = (number1 - number2);
        if (difference > 1)
        {
            return number1;
        }
        else
        {
            return number2;
        }
    }

    //Prints out some text as many times as the number provided:
    void MyFunction(string wordToPrint, int timesToRepeat)
    {
        for (int i = 0; i < timesToRepeat; i++)
        {
            print(wordToPrint);
        }
    }

    //Returns the factorial of a number:
    int Factorial(int timesToRepeat)
    {
        int result = 1;
        for (int i = 1; i <= timesToRepeat; i++)
        {
            print("old " + result);
            print("step " + i);
            result = result * i;
            print("new" + result);
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
            //MyFunction(myWord, myNumber1);
            //IsEven();
            /*
            int result = WhatisBigger(myNumber1, myNumber2);
            print(result);
            */

            Factorial(myNumber1);


        }
    }
}
