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

    [Header("Slide")]
    public float slideForce = 3f;

    [Header("Lanes")]
    public Transform[] laneMarkers;
    private int currentLane = 1;
    private Vector3 targetPosition;

    [Header("Animation")]
    public Animator animator;

    [Header("Collision Settings")]
    public string obstacleTag = "Obstacle";

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => OnMove(ctx);
        controls.Player.Jump.performed += ctx => OnJump(ctx);
        controls.Player.Duck.performed += ctx => OnSlide(ctx);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Setup lane markers
        if (laneMarkers != null && laneMarkers.Length >= 3)
        {
            System.Array.Sort(laneMarkers, (a, b) => a.position.x.CompareTo(b.position.x));
            laneDistance = Mathf.Abs(laneMarkers[2].position.x - laneMarkers[1].position.x);
            laneOffset = laneMarkers[1].position.x;
        }

        targetPosition = transform.position;
        targetPosition.x = laneOffset + (currentLane - 1) * laneDistance;

        // animator start
        if (animator != null)
        {
            animator.SetBool("StartRunning", true);
            animator.SetBool("IsRunning", true);
        }
    }

    private void Update()
    {
        // Forward movement
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Smooth lane transition
        targetPosition = new Vector3(
            laneOffset + (currentLane - 1) * laneDistance,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
    }

    // -----------------------
    // Input Handling
    // -----------------------

    private void OnMove(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Vector2 input = ctx.ReadValue<Vector2>();

        if (input.x > 0.5f && currentLane < 2)
            currentLane++;
        else if (input.x < -0.5f && currentLane > 0)
            currentLane--;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            animator.SetTrigger("IsJumping");
        }
    }

    private void OnSlide(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !isGrounded)
            return;

        // trigger sliding
        animator.SetTrigger("IsSliding");
        animator.SetTrigger("StaySliding");

        rb.AddForce(Vector3.down * slideForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // -----------------------
    // Collision Handling
    // -----------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            return;
        }

        if (collision.gameObject.CompareTag(obstacleTag))
        {
            animator.SetTrigger("ObstacleLeftEncountered");
            animator.SetTrigger("ObstacleRightEncountered");
            animator.SetTrigger("ObstacleLeftEncountered");

            GameOver();
        }
    }

    private void GameOver()
    {
        forwardSpeed = 0f;

        rb.linearVelocity = Vector3.zero;

        animator.SetBool("IsRunning", false);

        if (BikeGameManager.Instance != null)
            BikeGameManager.Instance.EndGame();
    }
}

