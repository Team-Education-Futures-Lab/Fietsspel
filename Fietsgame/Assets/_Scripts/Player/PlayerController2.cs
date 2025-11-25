using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
{
    #region Singleton
    private static PlayerController _instance;
    public static PlayerController Instance => _instance ??= FindObjectOfType<PlayerController>() ?? new GameObject(typeof(PlayerController).Name).AddComponent<PlayerController>();
    #endregion

    [Header("Movement Settings")]
    public float moveSpeed;
    public float laneDistance;
    [SerializeField] private int currentLane;
    private bool isLaneSwitchingCooldown;
    private float laneSwitchCooldownDuration;

    [Header("Jump Settings")]
    public float jumpForce;
    public float maxJumpHeight;
    public float gravity;
    public float descentForce;
    public float jumpCooldown;
    public float gravityMultiplier = 2f;
    private float normalGravity;
    private bool isJumping;
    private bool isGrounded;
    private bool canJump = true;

    [Header("Slide Settings")]
    public float slideDuration;
    private bool isSliding;
    [SerializeField] private CapsuleCollider _PlayerCollider;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float airSlideDescentMultiplier = 3f;

    [Header("References")]
    public Animator animator;
    public string groundTag;
    [SerializeField] private CheckForPath _checkforpath;

    private Rigidbody rb;

    private void Start()
    {
        SetInitialPosition();
        animator = GetComponent<Animator>();
        normalGravity = gravity;
        rb = GetComponent<Rigidbody>();

        originalColliderHeight = _PlayerCollider.height;
        originalColliderCenter = _PlayerCollider.center;
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
        HandleLaneMovement();
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

    private void HandleLaneMovement()
    {
        if (isLaneSwitchingCooldown) return;

        if (Input.GetKeyDown(KeyCode.A)) MoveLane(-1);
        else if (Input.GetKeyDown(KeyCode.D)) MoveLane(1);
    }

    private void MoveLane(int direction)
    {
        int targetLane = Mathf.Clamp(currentLane + direction, 0, 2);
        currentLane = targetLane;
        StartCoroutine(MoveToLane(currentLane * laneDistance));
        StartCoroutine(LaneSwitchCooldown());
    }

    private IEnumerator MoveToLane(float targetX)
    {
        float duration = 0.2f;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        Vector3 startPosition = transform.position;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, t / duration);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private IEnumerator LaneSwitchCooldown()
    {
        isLaneSwitchingCooldown = true;
        yield return new WaitForSeconds(laneSwitchCooldownDuration);
        isLaneSwitchingCooldown = false;
    }

    private void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0)) && canJump && isGrounded)
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

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(2f * jumpForce * normalGravity), rb.linearVelocity.z);

            isSliding = false;
        }
        else
        {
            animator.SetTrigger("IsJumping");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(2f * jumpForce * normalGravity), rb.linearVelocity.z);
        }

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
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.JoystickButton2)) && !isSliding)
        {
            StartCoroutine(SlideRoutine());StartCoroutine(SlideRoutine());
        }
    }

    private IEnumerator SlideRoutine()
    {
        isSliding = true;
        animator.SetTrigger("IsSliding");

        _PlayerCollider.height = originalColliderHeight * 0.5f;
        _PlayerCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y - (originalColliderHeight * 0.25f), originalColliderCenter.z);

        yield return new WaitForSeconds(slideDuration);

        _PlayerCollider.height = originalColliderHeight;
        _PlayerCollider.center = originalColliderCenter;
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

    private void SetInitialPosition()
    {
        transform.position = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
    }

}