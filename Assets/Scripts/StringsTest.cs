using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        print("this is some text.");
        print("My "+"test"+"text"); //My testtext
        print("elephant"[6]); //n
        print("Horse".Substring(1,3)); //ors
        print("Machine".Length); //7
        */

        print(100%3); //1
        print(("orange"+"pink")[7]); //i
        print("black and white".Substring(6,3)); //and
        print("programming"["hello".Length % "hi".Length + 7]); //i
        print("big".ToUpper());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
