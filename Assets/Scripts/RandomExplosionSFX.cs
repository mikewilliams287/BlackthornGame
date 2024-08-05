using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExplosionSFX : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        int randomSound = Random.Range(0, soundEffects.Length);
        source.clip = soundEffects[randomSound];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
