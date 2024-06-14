using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradingTest : MonoBehaviour
{
    [Range(0f,100f)]
    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        if(score >= 0 && score <= 100){
            if(score>=90f){
                print("The grade is an A.");

            }else if(score>=80f){
                print("The grade is a B.");

            }else if(score>=70f){
                print("The grade is a C.");
            }else if(score>=65f){
                print("The grade us a D.");
            }else{
                print("The grade is an F.");
            }
        }
        else{
            print("Score must be between 0 and 100");
        }

        }
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
