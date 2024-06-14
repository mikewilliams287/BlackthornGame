using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsPractice : MonoBehaviour
{
    public int seed;
    int seedInverse;
    int max;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
    
        seedInverse = seed*-1;
        max = seed+99;
        
    
    }

    // Update is called once per frame
    void Update()
    {
        
        //Print all numbers between -X and X:
        /*
        while(seedInverse <= seed){
            print(seedInverse);
            seedInverse += 1;
        }
        */

        //Print all even numbers between X and X*5:
        /*
        while((seed+counter) <= max){
            print(seed+counter);
            counter += 2;
        }
        */

        //Print numbers between 1 and 100 that are divisable by 5 and 3;

        for(int i = 100; seed <= i; seed += 1){
            if(seed%5 == 0 && seed%3 == 0){
                print(seed);
            }
        }

    }
}
