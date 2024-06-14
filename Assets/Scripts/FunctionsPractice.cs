using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionsPractice : MonoBehaviour
{

    public int myNumber1;
    public int myNumber2;

    // Returns true if number is even and false if number is odd
    void IsEven(){
        if(myNumber1%2 == 0){
            print(myNumber1 + " is and EVEN number.");
        }else{
            print(myNumber1 + " is an ODD number");
        }
        }

    // Returns the biggest number out of the two:

    int WhatisBigger(int number1, int number2){
        int difference = (number1-number2);
        if(difference > 1){
            return number1;
        }else{
            return number2;
        }

    }

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
        //IsEven();
        int result = WhatisBigger(myNumber1, myNumber2);
        print(result);
        

       } 
    }
}
