using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController _instance;
    public static PlayerController Instance => _instance ??= FindObjectOfType<PlayerController>() ?? new GameObject(typeof(PlayerController).Name).AddComponent<PlayerController>();
    #endregion

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float gravity = 9.81f;
    public float gravityMultiplier = 2f;
    public float jumpCooldown = 0.3f;
    private float normalGravity;
    private bool isJumping;
    private bool isGrounded;
    private bool canJump = true;

    [Header("Slide Settings")]
    public float slideDuration = 1f;
    public float airSlideDescentMultiplier = 3f;
    private bool isSliding;
    [SerializeField] private CapsuleCollider _playerCollider;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    [Header("References")]
    public Animator animator;
    public string groundTag;
    //[SerializeField] private CheckForPath _checkforpath;

    private Rigidbody rb;

    // Input system fields
    private PlayerInput _playerInput;
    private InputAction _jumpAction;
    private InputAction _slideAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing! Please add one to the Player object.");
            return;
        }

        _jumpAction = _playerInput.actions["Jump"];
        _slideAction = _playerInput.actions["Slide"];
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        normalGravity = gravity;

        originalColliderHeight = _playerCollider.height;
        originalColliderCenter = _playerCollider.center;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Start Running") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            animator.SetBool("IsRunning", true);
        }

        if (EndlessRunner.Instance.hasStarted && !_checkforpath.hasDied)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        HandleGravity();
        HandleJump();
        HandleSlide();
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            if (isSliding)
            {
                rb.linearVelocity += Vector3.down * gravity * airSlideDescentMultiplier * Time.deltaTime;
            }
            else
            {
                rb.linearVelocity += Vector3.down * gravity * gravityMultiplier * Time.deltaTime;
            }
        }
    }

    private void HandleJump()
    {
        if (_jumpAction.WasPressedThisFrame() && canJump && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isSliding)
        {
            animator.ResetTrigger("StaySliding");
            animator.ResetTrigger("IsSliding");
            animator.SetTrigger("IsJumping");
            isSliding = false;
        }
        else
        {
            animator.SetTrigger("IsJumping");
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(2f * jumpForce * normalGravity), rb.linearVelocity.z);

        isJumping = true;
        isGrounded = false;

        StartCoroutine(JumpCooldown());
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void HandleSlide()
    {
        if (_slideAction.WasPressedThisFrame() && !isSliding)
        {
            StartCoroutine(SlideRoutine());
        }
    }

    private IEnumerator SlideRoutine()
    {
        isSliding = true;
        animator.SetTrigger("IsSliding");

        _playerCollider.height = originalColliderHeight * 0.5f;
        _playerCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y - (originalColliderHeight * 0.25f), originalColliderCenter.z);

        yield return new WaitForSeconds(slideDuration);

        _playerCollider.height = originalColliderHeight;
        _playerCollider.center = originalColliderCenter;
        isSliding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
            isJumping = false;
            animator.ResetTrigger("IsJumping");
        }
    }
}
