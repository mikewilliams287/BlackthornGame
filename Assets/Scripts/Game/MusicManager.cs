using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource source;
    public AudioClip[] audioClips;

    private void Awake()
    {
        //source = GetComponent<AudioSource>();
        //source.clip = audioClips[0];
        //source.Play();
        if (instance == null)
        {
            //source = GetComponent<AudioSource>();
            //source.clip = audioClips[0];
            //source.Play();
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX()
    {
        source = GetComponent<AudioSource>();
        source.clip = audioClips[1];
        source.Play();
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
