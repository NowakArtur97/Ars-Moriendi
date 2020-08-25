using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float extraHeightGroundCheck = 0.1f;
    [SerializeField] private float extrWidthtWallCheck = 0.2f;

    [SerializeField] private Transform airJumpParticleEffect;
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float jumpSpeed = 17f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float wallSlideSpeed = 1f;

    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 2);
    [SerializeField] private float wallJumpForce = 20;

    [SerializeField] private float prematureJumpAttemptDefaultTimer = 0.15f;
    private float prematureJumpAttemptTimer = 0;
    private bool isAttemptingToJump;

    private float facingDirection = 1;
    private bool isFacingRight = true;
    private float defaultGravityScale;
    private int airJumpCount;
    private readonly int airJumpCountMax = 0;

    private bool isGrounded = true;
    private bool isWalking = false;
    private bool isWallSliding = false;
    private bool isTouchingLedge = false;
    private bool isLedgeClimbingActive = false;
    private float movementInput;

    [SerializeField] private float ledgeClimbXOffset1 = 0f;
    [SerializeField] private float ledgeClimbXOffset2 = 0f;
    [SerializeField] private float ledgeClimbYOffset1 = 0f;
    [SerializeField] private float ledgeClimbYOffset2 = 0f;

    private Vector2 positionBeforeClimbing;
    private Vector2 ledgePositionBottom;
    private Vector2 ledgePosition1;
    private Vector2 ledgePosition2;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myBoxCollider2D;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<Collider2D>();
        myAnimator = GetComponentInChildren<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        controls = new InputMaster();
    }

    private void Start()
    {
        defaultGravityScale = myRigidbody2D.gravityScale;
        controls.Player.Jump.performed += _ => JumpHandler();
        controls.Player.Dash.performed += _ => Dash();

        wallJumpDirection.Normalize();
    }

    public void TouchedJumpOrb()
    {
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        airJumpCount = 0;
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        CheckMovement();

        Move();
        SlideWall();
    }

    private void CheckMovement()
    {
        CheckPrematureJump();

        CheckWallMovement();

        CheckGroundMovement();

        CheckLedgeMovement();
    }

    private void CheckGroundMovement()
    {
        if (IsTouchingGround())
        {
            airJumpCount = 0;
            isWalking = movementInput != 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            isWalking = false;
        }
    }

    private void CheckPrematureJump()
    {
        if (isAttemptingToJump)
        {
            prematureJumpAttemptTimer -= Time.deltaTime;
        }

        if (isAttemptingToJump && isGrounded && prematureJumpAttemptTimer > 0)
        {
            NormalJump();
        }
        else if (isAttemptingToJump && isWallSliding && prematureJumpAttemptTimer > 0)
        {
            WallJump();
        }
    }

    private void CheckWallMovement()
    {
        if (IsWallSliding())
        {
            airJumpCount = 0;
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckLedgeMovement()
    {
        if (IsTouchingLedge() && !isLedgeClimbingActive)
        {
            ClimbLedge();
        }
    }

    private void UpdateAnimations()
    {
        myAnimator.SetBool("isGrounded", isGrounded);
        myAnimator.SetBool("isWalking", isWalking);
        myAnimator.SetFloat("yVelocity", myRigidbody2D.velocity.y);
        myAnimator.SetBool("isWallSliding", isWallSliding);
    }

    private void Move()
    {
        movementInput = controls.Player.HorizontalMovement.ReadValue<float>();

        if (movementInput == 0) { return; }

        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * runSpeed * Time.deltaTime;
        transform.position = currentPosition;
        CheckDirection();
    }

    private void JumpHandler()
    {
        if (prematureJumpAttemptTimer <= 0)
        {
            if (isGrounded)
            {
                NormalJump();
            }
            else if (isWallSliding && !isAttemptingToJump)
            {
                WallJump();
                FlipSprite();
            }
            else if (airJumpCountMax >= ++airJumpCount)
            {
                AirJump();
            }
            else if (airJumpCountMax < ++airJumpCount)
            {
                isAttemptingToJump = true;
                prematureJumpAttemptTimer = prematureJumpAttemptDefaultTimer;
            }
        }
    }

    private void NormalJump()
    {
        myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

        isAttemptingToJump = false;
        prematureJumpAttemptTimer = 0;
    }

    private void WallJump()
    {
        myRigidbody2D.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);

        isWallSliding = false;
        isAttemptingToJump = false;
        prematureJumpAttemptTimer = 0;
    }

    private void ClimbLedge()
    {
        positionBeforeClimbing = transform.position;
        controls.Disable();

        if (isFacingRight)
        {
            ledgePosition1 = new Vector2(
                Mathf.Floor(positionBeforeClimbing.x + extrWidthtWallCheck) - ledgeClimbXOffset1,
                Mathf.Floor(positionBeforeClimbing.y) + ledgeClimbYOffset1);

            ledgePosition2 = new Vector2(
                Mathf.Floor(positionBeforeClimbing.x + extrWidthtWallCheck) + ledgeClimbXOffset2,
                Mathf.Floor(positionBeforeClimbing.y) + ledgeClimbYOffset2);
        }
        else
        {
            ledgePosition1 = new Vector2(
                Mathf.Ceil(positionBeforeClimbing.x - extrWidthtWallCheck) + ledgeClimbXOffset1,
                Mathf.Floor(positionBeforeClimbing.y) + ledgeClimbYOffset1);

            ledgePosition2 = new Vector2(
                Mathf.Ceil(positionBeforeClimbing.x - extrWidthtWallCheck) - ledgeClimbXOffset2,
                Mathf.Floor(positionBeforeClimbing.y) + ledgeClimbYOffset2);
        }

        transform.position = ledgePosition1;
        myRigidbody2D.velocity = Vector3.zero;
        myRigidbody2D.gravityScale = 0;

        isTouchingLedge = true;
        isWallSliding = false;
        isLedgeClimbingActive = true;

        myAnimator.SetBool("isLedgeClimbing", isTouchingLedge);
    }

    private void FinishClimbEdge()
    {
        myRigidbody2D.gravityScale = defaultGravityScale;
        transform.position = ledgePosition2;
        controls.Enable();

        isLedgeClimbingActive = false;
        isTouchingLedge = false;

        myAnimator.SetBool("isLedgeClimbing", isTouchingLedge);
    }

    private void AirJump()
    {
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        Instantiate(airJumpParticleEffect, transform.position, Quaternion.identity);
    }

    private void SlideWall()
    {
        if (isWallSliding)
        {
            if (myRigidbody2D.velocity.y < -wallSlideSpeed)
            {
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Dash()
    {
        if (IsTouchingGround()) { return; }

        myRigidbody2D.gravityScale = 0;
        myRigidbody2D.velocity = new Vector2(facingDirection * dashSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.gravityScale = defaultGravityScale;
    }

    private void CheckDirection()
    {
        if (HasTurnedAround())
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        mySpriteRenderer.transform.Rotate(Vector3.up * -180);
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
    }

    private bool IsTouchingGround()
    {
        Vector2 raycastSize = new Vector2(myBoxCollider2D.bounds.size.x / 2, myBoxCollider2D.bounds.size.y);
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, raycastSize, 0f, Vector2.down, extraHeightGroundCheck, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.green : Color.red;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, Vector2.down * (myBoxCollider2D.bounds.extents.y + extraHeightGroundCheck), rayColor);

        return raycastHit2D.collider != null;
    }

    private bool IsTouchingWall()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        Vector2 raycastSize = new Vector2(myBoxCollider2D.bounds.size.x, myBoxCollider2D.bounds.size.y / 2);
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, raycastSize, 0f, direction, extrWidthtWallCheck, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.blue : Color.yellow;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, direction * (myBoxCollider2D.bounds.extents.x + extraHeightWallCheck), rayColor);

        return raycastHit2D.collider != null;
    }

    private bool IsWallSliding()
    {
        return IsTouchingWall() && !isGrounded && myRigidbody2D.velocity.y < 0;
    }

    private bool IsTouchingLedge()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        Vector2 lowerRaycastVector = myBoxCollider2D.bounds.center;
        RaycastHit2D raycastHitWall2D = Physics2D.Raycast(lowerRaycastVector, direction, myBoxCollider2D.bounds.extents.y + extrWidthtWallCheck, ground);

        Vector2 higherRaycastVector = new Vector2(myBoxCollider2D.bounds.center.x, transform.position.y + myBoxCollider2D.bounds.extents.y);
        RaycastHit2D raycastHitEmpty2D = Physics2D.Raycast(higherRaycastVector, direction, myBoxCollider2D.bounds.extents.y + extrWidthtWallCheck, ground);

        if (raycastHitWall2D.collider != null)
        {
            ledgePositionBottom = raycastHitWall2D.collider.transform.position;
        }

        Color rayColor = raycastHitWall2D.collider != null ? Color.yellow : Color.green;
        Color rayColor2 = raycastHitEmpty2D.collider == null ? Color.blue : Color.red;

        Debug.DrawRay(lowerRaycastVector, direction, rayColor);
        Debug.DrawRay(higherRaycastVector, direction, rayColor2);

        return raycastHitWall2D.collider != null && raycastHitEmpty2D.collider == null;
    }

    private bool HasTurnedAround()
    {
        return (isFacingRight && movementInput < 0) || (!isFacingRight && movementInput > 0);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
