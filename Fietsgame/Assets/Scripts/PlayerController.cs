using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float laneDistance = 3f;   // Distance between lanes (used if no markers)
    public float laneSwitchSpeed = 10f;
    public float laneOffset = 0f;     // Middle lane X offset (used if no markers)

    [Header("Jump")]
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;

    [Header("Lanes")]
    public Transform[] laneMarkers;   // Optional: assign Lane0, Lane1, Lane2 in inspector

    private int currentLane = 1;      // 0 = left, 1 = middle, 2 = right
    private Vector3 targetPosition;

    [Header("Animation")]
    public Animator animator;

    private PlayerControls controls;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Auto-detect lanes if markers are assigned
        if (laneMarkers != null && laneMarkers.Length >= 3)
        {
            // Sort markers by X position (left → right)
            System.Array.Sort(laneMarkers, (a, b) => a.position.x.CompareTo(b.position.x));

            // Calculate lane distance and offset
            laneDistance = Mathf.Abs(laneMarkers[2].position.x - laneMarkers[1].position.x);
            laneOffset = laneMarkers[1].position.x; // middle lane
        }

        // Set starting lane
        targetPosition = transform.position;
        targetPosition.x = laneOffset + (currentLane - 1) * laneDistance;
    }

    void Update()
    {
        // Always move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Smoothly move to lane position
        targetPosition = new Vector3(
            laneOffset + (currentLane - 1) * laneDistance,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);

        // Animator parameters
        if (animator != null)
        {
            animator.SetBool("IsJumping", !isGrounded);
            animator.SetFloat("Speed", forwardSpeed);
        }
    }

    // Input System: called when player swipes/moves left/right
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Vector2 input = ctx.ReadValue<Vector2>();
        Debug.Log("Move input: " + input);

        if (input.x > 0.5f && currentLane < 2) // move right
        {
            currentLane++;
            Debug.Log("Move right -> lane: " + currentLane);
        }
        else if (input.x < -0.5f && currentLane > 0) // move left
        {
            currentLane--;
            Debug.Log("Move left -> lane: " + currentLane);
        }
    }

    // Input System: called when player jumps
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => OnMove(ctx);
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();
}
