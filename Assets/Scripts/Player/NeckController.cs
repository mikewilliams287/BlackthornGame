using System.Collections;
using UnityEngine;
using UnityEngine.U2D.IK;

public class NeckController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CCDSolver2D neckIKSolver;
    [SerializeField] private BrontoPlayerMovement playerMovement;
    [SerializeField] private Transform neckBase; // The root bone of the neck chain
    [SerializeField] private Transform headPositionReference; // Visual editor reference for head position

    [Header("Targets Positioning")]
    [SerializeField]
    private bool useRelativePositioning = true; // Option for relative vs absolute

    [Header("IK Targets")]
    [SerializeField] private Transform physicsTarget; // Target for physics based movement
    [SerializeField] private Transform whipTarget; // Target for head whip movement
    [SerializeField] private Transform blendTarget; // The actual target the IK solver follows

    [Header("Mode Settings")]
    [SerializeField] private float blendSpeed = 5f; // Speed of transitions between modes
    [SerializeField] private bool debugMode = false; // For testing purposes

    // Current state
    public enum NeckMode
    {
        Physics, // Following physics based movement
        Whipping, // Performing head whip
        Blending, // Transitioning back to physics
    }

    [SerializeField] private NeckMode currentMode = NeckMode.Physics; // Default start in Physics mode

    // Internal references
    private PhysicsNeckBehavior physicsNeck;
    private HeadWhipController whipController;

    private void OnValidate()
    {
        // This runs in the editor when values change
        if (headPositionReference != null && Application.isPlaying == false)
        {
            // Preview the position in edit mode
            if (physicsTarget != null) physicsTarget.position = headPositionReference.position;
            if (whipTarget != null) whipTarget.position = headPositionReference.position;
            if (blendTarget != null) blendTarget.position = headPositionReference.position;
        }
    }

    void Start()
    {
        InitializeComponents();
        SetupTargets();

    }

    void Update()
    {
        UpdateNeckTarget();

        if (debugMode)
        {
            DebugCurrentMode();
        }
    }

    private void InitializeComponents()
    {
        // Get or add the physics neck behavior
        physicsNeck = GetComponent<PhysicsNeckBehavior>();
        if (physicsNeck == null)
        {
            physicsNeck = gameObject.AddComponent<PhysicsNeckBehavior>();
        }

        // Get or add the head whip controller
        whipController = GetComponent<HeadWhipController>();
        if (whipController == null)
        {
            whipController = gameObject.AddComponent<HeadWhipController>();
        }

        // Validate required references
        if (neckIKSolver == null)
        {
            Debug.LogError("NeckController: CCDSolver2D reference is missing!");
        }
        if (playerMovement == null)
        {
            playerMovement = GetComponent<BrontoPlayerMovement>();
        }
    }

    private void SetupTargets()
    {
        // Create target game objects if they dont exist
        if (physicsTarget == null)
        {
            GameObject physicsGameObject = new GameObject("PhysicsTarget");
            physicsTarget = physicsGameObject.transform;
            physicsTarget.SetParent(transform);
        }

        if (whipTarget == null)
        {
            GameObject whipGameObject = new GameObject("WhipTarget");
            whipTarget = whipGameObject.transform;
            whipTarget.SetParent(transform);
        }

        if (blendTarget == null)
        {
            GameObject blendGameObject = new GameObject("BlendTarget");
            blendTarget = blendGameObject.transform;
            blendTarget.SetParent(transform);
        }

        // Set the IK solver to follow the blend target
        if (neckIKSolver != null && neckIKSolver.GetChain(0) != null)
        {
            neckIKSolver.GetChain(0).target = blendTarget;
        }

        // Initialize positions using visual reference
        InitializeTargetPositions();
    }

    private void InitializeTargetPositions()
    {
        Vector3 targetPosition;

        if (headPositionReference == null)
        {
            Debug.LogError("Head Position Reference is missing! Please assign it in the inspector.");
            targetPosition = transform.position;
        }
        else
        {
            if (useRelativePositioning)
            {
                // Store relative position from character center
                Vector3 relativePos = headPositionReference.position - transform.position;
                targetPosition = transform.position + relativePos;
            }
            else
            {
                // Use absolute world position
                targetPosition = headPositionReference.position;
            }
        }

        // Set all targets to the same starting position
        physicsTarget.position = targetPosition;
        whipTarget.position = targetPosition;
        blendTarget.position = targetPosition;

    }

    private void UpdateNeckTarget()
    {
        switch (currentMode)
        {
            case NeckMode.Physics:
                // Blend target follows physics target
                blendTarget.position = Vector3.Lerp(blendTarget.position, physicsTarget.position, blendSpeed * Time.deltaTime);
                break;

            case NeckMode.Whipping:
                // Blend target follows whip target
                blendTarget.position = Vector3.Lerp(blendTarget.position, whipTarget.position, blendSpeed * Time.deltaTime);
                break;

            case NeckMode.Blending:
                // Gradually return to physics target
                blendTarget.position = Vector3.Lerp(blendTarget.position, physicsTarget.position, blendSpeed * Time.deltaTime);

                // Check if we are close enough to switch back to physics mode
                if (Vector3.Distance(blendTarget.position, physicsTarget.position) < 0.1f)
                {
                    SetMode(NeckMode.Physics);
                }
                break;
        }
    }

    public void SetMode(NeckMode newMode)
    {
        if (currentMode != newMode)
        {
            currentMode = newMode;

            if (debugMode)
            {
                Debug.Log($"NeckController: Switched to {newMode} mode");
            }
        }
    }

    public NeckMode GetCurrentMode()
    {
        return currentMode;
    }

    public bool CanPerformWhip()
    {
        return currentMode == NeckMode.Physics;
    }

    public void StartHeadWhip()
    {
        if (CanPerformWhip())
        {
            SetMode(NeckMode.Whipping);
        }
    }

    public void EndHeadWhip()
    {
        if (currentMode == NeckMode.Whipping)
        {
            SetMode(NeckMode.Blending);
        }
    }

    private void DebugCurrentMode()
    {
        // Visual debug info (you can see this in the inspector)
        name = $"NeckController [{currentMode}]";
    }

    // Public getters for other components
    public Transform GetPhysicsTarget() => physicsTarget;
    public Transform GetWhipTarget() => whipTarget;
    public Transform GetBlendTarget() => blendTarget;
    public BrontoPlayerMovement GetPlayerMovement() => playerMovement;


    private void OnDrawGizmos()
    {
        if (headPositionReference != null)
        {
            // Draw a sphere at the reference position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(headPositionReference.position, 0.2f);

            // Draw line from neck base to head reference
            if (neckBase != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(neckBase.position, headPositionReference.position);
            }
        }
    }
}
