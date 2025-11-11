using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float laneDistance = 3f;
    public float laneSwitchSpeed = 10f;
    public float laneOffset = 0f;

    [Header("Jump")]
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;

    [Header("Duck")]
    public float duckForce = 3f;

    [Header("Lanes")]
    public Transform[] laneMarkers;
    private int currentLane = 1;
    private Vector3 targetPosition;

    [Header("Animation")]
    public Animator animator;

    [Header("Collision Settings")]
    [SerializeField] private string obstacleTag = "Obstacle";

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => OnMove(ctx);
        controls.Player.Jump.performed += ctx => OnJump(ctx);
        controls.Player.Duck.performed += ctx => OnDuck(ctx);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (laneMarkers != null && laneMarkers.Length >= 3)
        {
            // Sort lane markers left to right
            System.Array.Sort(laneMarkers, (a, b) => a.position.x.CompareTo(b.position.x));

            laneDistance = Mathf.Abs(laneMarkers[2].position.x - laneMarkers[1].position.x);
            laneOffset = laneMarkers[1].position.x;
        }

        targetPosition = transform.position;
        targetPosition.x = laneOffset + (currentLane - 1) * laneDistance;
    }

    private void Update()
    {
        // Forward movement
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Smooth lane switching
        targetPosition = new Vector3(
            laneOffset + (currentLane - 1) * laneDistance,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);

        // Update animator parameters
        if (animator != null)
        {
            animator.SetBool("IsJumping", !isGrounded);
            animator.SetFloat("Speed", forwardSpeed);
            animator.SetBool("IsDucking", !isGrounded);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Vector2 input = ctx.ReadValue<Vector2>();
        Debug.Log("Move input: " + input);

        if (input.x > 0.5f && currentLane < 2)
        {
            currentLane++;
            Debug.Log("Move right -> lane: " + currentLane);
        }
        else if (input.x < -0.5f && currentLane > 0)
        {
            currentLane--;
            Debug.Log("Move left -> lane: " + currentLane);
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void OnDuck(InputAction.CallbackContext ctx)
    {
       

        if (ctx.performed && isGrounded)
        {
            Debug.Log("You duck");
            rb.AddForce(Vector3.down * duckForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag(obstacleTag))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        forwardSpeed = 0f;

        if (BikeGameManager.Instance != null)
        {
            BikeGameManager.Instance.EndGame();
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}
