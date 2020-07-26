using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private readonly float runSpeed = 5f;

    private InputMaster controls;

    private Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        controls = new InputMaster();
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

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
