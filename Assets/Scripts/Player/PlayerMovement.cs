using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform airJumpParticleEffect;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 100f;
    [SerializeField] private float dashSpeed = 10f;
    private DashDirection dashDirection;

    private bool isFacingRight = true;
    private float defaultGravityScale;
    private int airJumpCount;
    private readonly int airJumpCountMax = 2;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myBoxCollider2D;
    private SpriteRenderer mySpriteRenderer;

    private enum DashDirection
    {
        LEFT, RIGHT
    }

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<Collider2D>();
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

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            airJumpCount = 0;
        }

        Move();
    }

    private void Move()
    {
        float movementInput = controls.Player.HorizontalMovement.ReadValue<float>();
        SetDashDirection(movementInput);
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * runSpeed * Time.deltaTime;
        transform.position = currentPosition;
        CheckDirection(movementInput);
    }

    private void Jump()
    {
        if (IsGrounded())
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

    private void Dash()
    {
        Vector2 directionVector = dashDirection == DashDirection.LEFT ? Vector2.left : Vector2.right;
        myRigidbody2D.gravityScale = 0;
        myRigidbody2D.velocity = directionVector * dashSpeed;
        myRigidbody2D.gravityScale = defaultGravityScale;
    }

    private void SetDashDirection(float movementInput)
    {
        if (movementInput == 0) { return; }

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

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, myBoxCollider2D.bounds.size, 0f, Vector2.down, extraHeight, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.green : Color.red;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, Vector2.down * (myBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
        return raycastHit2D.collider != null;
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
