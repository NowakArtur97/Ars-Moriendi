using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float extraHeightGroundCheck = 0.1f;
    [SerializeField] private float extraHeightWallCheck = 0.3f;

    [SerializeField] private Transform airJumpParticleEffect;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 100f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float wallSlideSpeed = 1f;
    private DashDirection dashDirection;

    private bool isFacingRight = true;
    private float defaultGravityScale;
    private int airJumpCount;
    private readonly int airJumpCountMax = 2;

    private bool isGrounded = true;
    private bool isWalking = false;
    private bool isWallSliding = false;
    private float movementInput;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myBoxCollider2D;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private enum DashDirection
    {
        LEFT, RIGHT
    }

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
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Dash.performed += _ => Dash();
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
        if (IsTouchingGround())
        {
            airJumpCount = 0;
            isWalking = movementInput != 0;
            isGrounded = true;
        }
        else
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
            isGrounded = false;
            isWalking = false;
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

        SetDashDirection(movementInput);
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * runSpeed * Time.deltaTime;
        transform.position = currentPosition;
        CheckDirection(movementInput);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        else if (airJumpCountMax >= ++airJumpCount)
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            Instantiate(airJumpParticleEffect, transform.position, Quaternion.identity);
        }
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

        Vector2 directionVector = dashDirection == DashDirection.LEFT ? Vector2.left : Vector2.right;
        myRigidbody2D.gravityScale = 0;
        myRigidbody2D.velocity = directionVector * dashSpeed;
        myRigidbody2D.gravityScale = defaultGravityScale;
    }

    private void SetDashDirection(float movementInput)
    {
        dashDirection = movementInput == 1 ? DashDirection.RIGHT : DashDirection.LEFT;
    }

    private void CheckDirection(float movementInput)
    {
        if (HasTurnedAround(movementInput))
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        mySpriteRenderer.transform.Rotate(Vector3.up * -180);
        isFacingRight = !isFacingRight;
    }

    private bool IsTouchingGround()
    {
        extraHeightGroundCheck = 0.1f;
        Vector2 raycastSize = new Vector2(myBoxCollider2D.bounds.size.x / 2, myBoxCollider2D.bounds.size.y);
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, raycastSize, 0f, Vector2.down, extraHeightGroundCheck, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.green : Color.red;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, Vector2.down * (myBoxCollider2D.bounds.extents.y + extraHeightGroundCheck), rayColor);

        return raycastHit2D.collider != null;
    }

    private bool IsTouchingWall()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        extraHeightWallCheck = 0.3f;
        Vector2 raycastSize = new Vector2(myBoxCollider2D.bounds.size.x, myBoxCollider2D.bounds.size.y);
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, raycastSize, 0f, direction, extraHeightWallCheck, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.blue : Color.yellow;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, direction * (myBoxCollider2D.bounds.extents.x + extraHeightWallCheck), rayColor);

        return raycastHit2D.collider != null;
    }

    private bool IsWallSliding()
    {
        return IsTouchingWall() && !isGrounded && myRigidbody2D.velocity.y < 0;
    }

    private bool HasTurnedAround(float movementInput)
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
