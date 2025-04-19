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
        GameObject headTarget = Instantiate(headTargetPrefab, headEffector.position, Quaternion.identity);

        // Name it nicely
        headTarget.name = "Bronto_HeadTarget_Instance";

        // Set it as world object
        headTarget.transform.SetParent(null);

        // Hook up CCD Solver target
        ccdSolver.GetChain(0).target = headTarget.transform;
        ccdSolver.enabled = false; // force refresh
        ccdSolver.enabled = true;

        // Link RelativeJoin2D
        RelativeJoint2D joint = headTarget.GetComponent<RelativeJoint2D>();
        Rigidbody2D targetRb = headTarget.GetComponent<Rigidbody2D>();
        if (joint != null && targetRb != null && targetPivot != null)
        {
            joint.connectedBody = targetPivot;
        }
        else
        {
            Debug.LogWarning("Join or Rigidbody2D missing on head target, or TargetPivot not set");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
