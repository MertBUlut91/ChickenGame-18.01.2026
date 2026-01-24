using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform orientation;
    private Rigidbody rb;
    private StateController stateController;

    [Header("Movement")]
    private float horizontalInput, verticalInput;
    private Vector3 movementDirection;
    [SerializeField] private float movementSpeed;
    [SerializeField] KeyCode movementKey;

    [Header("Jump")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpDrag;


    [Header("Ground")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float groundDrag;

    [Header("Slide")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private bool isSliding;
    [SerializeField] private float slideDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        stateController = GetComponent<StateController>();
    }

    private void Update()
    {
        SetInput();
        SetPlayerDrag();
        SetState();
        //LimitPlyerSpeed();
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey))
        {
            isSliding = true;

        }
        else if (Input.GetKeyUp(slideKey))
        {
            isSliding = false;
        }
        else if (Input.GetKeyDown(movementKey))
        {
            isSliding = false;
        }
        else if (Input.GetKey(jumpKey) && canJump && IsGrounded())
        {
            canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), jumpCooldown);
        }
    }

    private void SetPlayerMovement()
    {
        movementDirection = orientation.forward * verticalInput
            + orientation.right * horizontalInput;

        float forceMultiplier = stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => slideMultiplier,
            PlayerState.Jump => airMultiplier,

            _ => 1f

        };

        rb.AddForce(movementDirection.normalized * movementSpeed * forceMultiplier,
            ForceMode.Force);

    }

    private void SetPlayerJumping()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f
            , rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, forceMode);
    }

    private void ResetJumping()
    {
        canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f
            , _groundLayer);
    }

    private void SetPlayerDrag()
    {
        rb.linearDamping = stateController.GetCurrentState() switch
        {
            PlayerState.Move => groundDrag,
            PlayerState.Slide => slideDrag,
            PlayerState.Jump => jumpDrag,
            _ => rb.linearDamping
        };
    }

    private void LimitPlyerSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        //Debug.Log("Flast Velocity : " + flatVelocity);
        //Debug.Log("Flat Velocity Mag : " + flatVelocity.magnitude);
        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;

            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y,
                limitedVelocity.z);

            //Debug.Log(flatVelocity.magnitude);
            //Debug.Log(limitedVelocity);

        }
    }

    private void SetState()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            stateController.ChanceState(newState);
        }

    }

    private Vector3 GetMovementDirection()
    {
        return movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return isSliding;
    }



}
