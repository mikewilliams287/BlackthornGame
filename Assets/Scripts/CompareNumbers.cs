using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareNumbers : MonoBehaviour
{
    public float numberA;
    public float numberB;
    // Start is called before the first frame update
    void Start()
    {
        if(numberA > numberB){
            print(numberA + " is larger than " + numberB);
        }else if(numberA < numberB){
            print(numberB + " is larger than " + numberA);
        }else{
            print("The numbers are equal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
