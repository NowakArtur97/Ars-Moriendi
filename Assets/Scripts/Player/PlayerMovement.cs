using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private LayerMask ground;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myCollider2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
        controls = new InputMaster();
    }

    private void Start()
    {
        controls.Player.Jump.performed += _ => Jump();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movementInput = controls.Player.HorizontalMovement.ReadValue<float>();
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * runSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            myRigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= myCollider2D.bounds.extents.x;
        topLeftPoint.y += myCollider2D.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += myCollider2D.bounds.extents.x;
        bottomRightPoint.y -= myCollider2D.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
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
