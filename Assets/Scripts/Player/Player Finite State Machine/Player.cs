using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private D_PlayerData _playerData;

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    public PlayerFiniteStateMachine PlayerFiniteStateMachine { get; private set; }

    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }

    public Animator MyAnmator { get; private set; }
    public Rigidbody2D MyRigidbody { get; private set; }
    public PlayerInputHandler PlayerInputHandler { get; private set; }

    private void Awake()
    {
        PlayerFiniteStateMachine = new PlayerFiniteStateMachine();

        PlayerIdleState = new PlayerIdleState(this, PlayerFiniteStateMachine, _playerData, "idle");
        PlayerMoveState = new PlayerMoveState(this, PlayerFiniteStateMachine, _playerData, "move");
    }

    private void Start()
    {
        MyAnmator = GetComponent<Animator>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();

        FacingDirection = 1;

        PlayerFiniteStateMachine.Initialize(PlayerIdleState);
    }

    private void Update()
    {
        CurrentVelocity = MyRigidbody.velocity;

        PlayerFiniteStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        PlayerFiniteStateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        MyRigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
}
