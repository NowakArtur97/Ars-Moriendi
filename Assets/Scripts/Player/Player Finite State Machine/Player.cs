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
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    [SerializeField]
    private Transform _attackPosition;

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
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerSwordAttackState SwordAttackState { get; private set; }
    public PlayerSwordAttackState_01 SwordAttackState01 { get; private set; }
    public PlayerSwordAttackState_02 SwordAttackState02 { get; private set; }
    public PlayerSwordAttackState_03 SwordAttackState03 { get; private set; }

    #endregion

    #region Components

    public Animator MyAnmator { get; private set; }
    public Rigidbody2D MyRigidbody { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }

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
        WallSlideState = new PlayerWallSlideState(this, FiniteStateMachine, _playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, FiniteStateMachine, _playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, FiniteStateMachine, _playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, FiniteStateMachine, _playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, FiniteStateMachine, _playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, FiniteStateMachine, _playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, FiniteStateMachine, _playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, FiniteStateMachine, _playerData, "crouchMove");
        SwordAttackState = new PlayerSwordAttackState(this, FiniteStateMachine, _playerData, "swordAttack01", _attackPosition, 1);
        SwordAttackState01 = new PlayerSwordAttackState_01(this, FiniteStateMachine, _playerData, "swordAttack01", _attackPosition, 1);
        SwordAttackState02 = new PlayerSwordAttackState_02(this, FiniteStateMachine, _playerData, "swordAttack02", _attackPosition, 2);
        SwordAttackState03 = new PlayerSwordAttackState_03(this, FiniteStateMachine, _playerData, "swordAttack03", _attackPosition, 3);
    }

    private void Start()
    {
        MyAnmator = GetComponent<Animator>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        DashDirectionIndicator = transform.Find("Dash Direction Indicator");

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

    public void SetVelocityZero()
    {
        MyRigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

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

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(velocity * angle.x * direction, velocity * angle.y);
        MyRigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = velocity * direction;
        MyRigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(_groundCheck.position, _playerData.groundCheckRadius, _playerData.whatIsGround);

    public bool CheckIfTouchingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);

    public bool CheckIfBackIsTouchingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);

    public bool CheckIfTouchingLedge() => Physics2D.Raycast(_ledgeCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);

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

    // TODO: REFACTOR (Attack State instead of Cuttent State)
    public virtual void TriggerAttack() => FiniteStateMachine.CurrentState.TriggerAttack();

    public virtual void FinishAttack() => FiniteStateMachine.CurrentState.FinishAttack();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);
        float xDistance = xHit.distance;

        _workspace.Set(xDistance * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)_workspace, Vector2.down, _ledgeCheck.position.y - _wallCheck.position.y,
            _playerData.whatIsGround);
        float yDistance = yHit.distance;

        _workspace.Set(_wallCheck.position.x + (xDistance * FacingDirection), _ledgeCheck.position.y - yDistance);

        return _workspace;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPosition.position, _playerData.attackRadius);
    }

    #endregion
}
