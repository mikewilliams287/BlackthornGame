using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    //public AudioSource startSound;

    public void OnPlayButtonClicked()
    {
        AudioManager.instance.PlayMusic(null);
        SceneManager.LoadScene("Game");
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
