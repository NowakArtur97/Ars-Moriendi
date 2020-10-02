using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private D_PlayerData _playerData;

    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform _groundCheck;

    #endregion

    #region Other Variables

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    #region State Variables

    public PlayerFiniteStateMachine FiniteStateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }

    #endregion

    #region Components

    public Animator MyAnmator { get; private set; }
    public Rigidbody2D MyRigidbody { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        FiniteStateMachine = new PlayerFiniteStateMachine();

        IdleState = new PlayerIdleState(this, FiniteStateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, FiniteStateMachine, _playerData, "move");
        LandState = new PlayerLandState(this, FiniteStateMachine, _playerData, "land");
        JumpState = new PlayerJumpState(this, FiniteStateMachine, _playerData, "inAir");
        InAirState = new PlayerInAirState(this, FiniteStateMachine, _playerData, "inAir");
    }

    private void Start()
    {
        MyAnmator = GetComponent<Animator>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();

        FacingDirection = 1;

        FiniteStateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = MyRigidbody.velocity;

        FiniteStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        FiniteStateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        MyRigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        MyRigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    #endregion

    #region Check Functions

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(_groundCheck.position, _playerData.groundCheckRadius, _playerData.whatIsGround);

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => FiniteStateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => FiniteStateMachine.CurrentState.AnimationFinishedTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion
}
