using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float laneDistance = 3f;
    public float laneSwitchSpeed = 10f;

    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;

    private int currentLane = 1;
    private Vector3 targetPosition;

    private PlayerControls controls;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Always move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Smoothly move to lane position


        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
    }

    // Called by Input System
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Vector2 input = ctx.ReadValue<Vector2>();
        Debug.Log("Move input: " +  input);

        if (input.x > 0.5f && currentLane < 2) // swipe/move right
        {
            currentLane++;
            Debug.Log("Move right -> lane: " + currentLane);
        }

        else if (input.x < -0.5f && currentLane > 0) // swipe/move left
        {
            currentLane--;
            Debug.Log("Move Left -> lane:" + currentLane);
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

    private void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        Debug.Log("OnMove: " + input);
    }
}
