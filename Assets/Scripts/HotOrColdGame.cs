using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HotOrColdGame : MonoBehaviour
{
    int randomNumber;

    [Range(0,100)]
    public int playerGuess;

    int numberOfAttemps; 
    
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0,101);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            numberOfAttemps += 1;
            if(playerGuess==randomNumber){
                print("You got it! The secret number is "+ randomNumber +". You got it in " + numberOfAttemps.ToString() + " attempts.");
            }else if(Mathf.Abs(randomNumber-playerGuess) <= 5){
                print("HOT!");
            }else if(Mathf.Abs(randomNumber-playerGuess) <= 10){
                print("Warm...");
            }else if(Mathf.Abs(randomNumber-playerGuess) <= 20){
                print("Mild...");
            }else if(Mathf.Abs(randomNumber-playerGuess) <= 40){
                print("Cold...");
            }else{
                print("Sorry, try again.");
            }
        }
    
    }
}
