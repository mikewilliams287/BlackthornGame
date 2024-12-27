using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesTest : MonoBehaviour
{
    public string firstName;
    public string lastName;
    public int birthYear;

    int yearNow = System.DateTime.Now.Year;

    //int randomIndex;


    // Start is called before the first frame update
    void Start()
    {
        print ("Your name is "+ firstName + " " + lastName);
        print ("Your initials are "+ firstName[0]+lastName[0]);
        print ("Your full name is "+ (firstName.Length + lastName.Length) + " chatacters long.");
        // randomIndex = Random.Range(0, firstName.Length);
        print ("Here is a random letter from your first name!: "+ firstName[Random.Range(0, firstName.Length)]);
        print("You were born in " + birthYear.ToString() + " ,which means that you are "+ (yearNow-birthYear) + " years old.");
        print ("You have been alive for "+ ((yearNow-birthYear)*365) + " days.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
