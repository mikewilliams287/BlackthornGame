using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{



    public static AudioManager instance;

    //public AudioClip mainMusic; // Reference to main music clip

    public AudioSource musicSource; // AudioSource for bkgd music
    public AudioSource sfxSource; // AudioSource for sfx


    public void PlayMusic(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            //musicSource.Stop();
            sfxSource.Play();
        }

        else
        {
            audioSource.Play();
        }

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            PlayMusic(musicSource);

        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Method to play sfx
    public void PlayButtonSound()
    {

        sfxSource.Play();

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
