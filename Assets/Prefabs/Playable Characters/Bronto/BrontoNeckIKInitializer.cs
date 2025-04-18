using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class BrontoNeckIKInitializer : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject headTargetPrefab;

    [Header("Internal References")]
    public CCDSolver2D ccdSolver;
    public Transform headEffector;
    public Rigidbody2D targetPivot;


    void Start()
    {
        // Instantiate the head target at the effector's position
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
