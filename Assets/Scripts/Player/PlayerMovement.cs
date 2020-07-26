using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 100f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform airJumpParticleEffect;

    private int airJumpCount;
    private readonly int airJumpCountMax = 2;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myBoxCollider2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<Collider2D>();
        controls = new InputMaster();
    }

    private void Start()
    {
        controls.Player.Jump.performed += ctx => Jump(ctx);
    }

    public void TouchedJumpOrb()
    {
        myRigidbody2D.velocity = Vector2.zero;
        myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        airJumpCount = 0;
    }

    private void Update()
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
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * runSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }

    private void Jump(CallbackContext context)
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

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(myBoxCollider2D.bounds.center, myBoxCollider2D.bounds.size, 0f, Vector2.down, extraHeight, ground);

        //Color rayColor = raycastHit2D.collider != null ? Color.green : Color.red;
        //Debug.DrawRay(myBoxCollider2D.bounds.center, Vector2.down * (myBoxCollider2D.bounds.extents.y + extraHeight), rayColor);
        return raycastHit2D.collider != null;
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
