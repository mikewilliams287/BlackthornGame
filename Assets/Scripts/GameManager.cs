using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void PlayGame()
    {
        //AudioManager.instance.PlayMusic(null);
        //AudioManager.instance.musicSource.Play();
        SceneManager.LoadScene("Game");
    }

    public void RestartGame()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        //This will close the game when built as an executable
        Application.Quit();

        //This will stop the play mode in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
