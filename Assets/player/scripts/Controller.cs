using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] static internal float currentSpeed = 3f;
    [SerializeField] static internal float walkSpeed = 4.0f;
    [SerializeField] static internal float runSpeed = 6f;
    [SerializeField] static internal float crouchSpeed = 1.5f;
    [SerializeField] private float gravity = 10.0f;
    internal bool isFreezed;
    internal bool isParalized;
    public GameObject blackScreen;

    [Header("Look parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 80.0f;

    [Header("Jump parameters")]
    [SerializeField] private float jumpForce = 3.0f;
    private PlayerStats playerStats;
    private float horizontal;
    private float vertical;

    private Camera playerCamera;
    [SerializeField] private GameObject cameraEmpty;
    private CharacterController characterController;

    public Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX;

    void Awake()
    {
        
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        StartCoroutine(blackScreen.GetComponent<BlackScreenScript>().ScreenCoroutine(1));
    }

    void Update()
    {
        if (!isParalized)
        {
            if (!isFreezed)
            {
                HandleMouseLock();
            }

            HandleMovementInput();
            // HandleJump();
            ApplyFinalMovement();
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }        
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2(currentSpeed * Input.GetAxis("Vertical"), currentSpeed * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = transform.TransformDirection(Vector3.forward) * currentInput.x + transform.TransformDirection(Vector3.right) * currentInput.y;
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLock()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        cameraEmpty.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpForce;
        }
    }

    private void ApplyFinalMovement()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift) && playerStats.endurance > 0.1f)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                currentSpeed = crouchSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
    }
}
