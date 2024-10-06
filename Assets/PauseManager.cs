using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    // Reference to the Pause Panel
    public GameObject pausepanel;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure PausePanel is disabled when the game starts
        pausepanel.SetActive(false);
        Time.timeScale = 1f; //Make sure the game is running at normal speed
    }


    void Update()
    {
        // Check if P or Esc key is pressed
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    // Method to toggle pause and resume
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f; // Stop the game by setting the timescale to 0
            pausepanel.SetActive(true); // Show the PausePanel
        }
        else
        {
            // Resume the game
            Time.timeScale = 1f; // Resume the game by setting the timescale to 1
            pausepanel.SetActive(false); // Hide the PausePanel
        }
    }

    // Method to resume the game

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausepanel.SetActive(false);
    }
}
