using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gem : MonoBehaviour, ICollectable
{

    public static event Action OnGemCollected;
    public void Collect()
    {
        Debug.Log("GEM COLLECTED");
        OnGemCollected?.Invoke();
        Destroy(gameObject);
    }



}
