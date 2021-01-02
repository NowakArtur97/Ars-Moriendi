using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Fields

    [Header("States Data")]
    [SerializeField]
    private D_PlayerBase _playerData;
    [SerializeField]
    private D_PlayerStats _playerStatsData;
    [SerializeField]
    private D_PlayerIdleState _idleStateData;
    [SerializeField]
    private D_PlayerMoveState _moveStateData;
    [SerializeField]
    private D_PlayerJumpState _jumpStateData;
    [SerializeField]
    private D_PlayerInAirState _inAirStateData;
    [SerializeField]
    private D_PlayerWallSlideState _wallSlideStateData;
    [SerializeField]
    private D_PlayerWallClimbState _wallClimbStateData;
    [SerializeField]
    private D_PlayerWallJumpState _wallJumpStateData;
    [SerializeField]
    private D_PlayerLedgeClimbState _ledgeClimbStateData;
    [SerializeField]
    private D_PlayerDashState _dashStateData;
    [SerializeField]
    private D_PlayerRollState _rollStateData;
    [SerializeField]
    private D_PlayerCrouchIdleState _crouchIdleStateData;
    [SerializeField]
    private D_PlayerCrouchMoveState _crouchMoveStateData;
    [SerializeField]
    private D_PlayerOnRopeState _onRopeStateData;
    [SerializeField]
    private D_PlayerSwordAttackState _swordAttackStateData01;
    [SerializeField]
    private D_PlayerSwordAttackState _swordAttackStateData02;
    [SerializeField]
    private D_PlayerSwordAttackState _swordAttackStateData03;
    [SerializeField]
    private D_PlayerBowArrowShotState _fireArrowShotStateData;
    [SerializeField]
    public D_PlayerStunState StunStateData;
    [SerializeField]
    private D_PlayerDeadState _deadStateData;

    [Header("UI")]
    [SerializeField]
    private HealthBar _playerHealthBar;

    #endregion

    #region Check Transforms

    [Header("Checks")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    [SerializeField]
    private Transform _ceilingCheck;
    [SerializeField]
    private Transform _swordAttackPosition01;
    [SerializeField]
    private Transform _swordAttackPosition02;
    [SerializeField]
    private Transform _swordAttackPosition03;
    [SerializeField]
    private Transform _fireArrowShotAttackPosition;
    #endregion

    #region State Variables

    public PlayerFiniteStateMachine FiniteStateMachine { get; private set; }
    public PlayerAnimationToStateMachine AnimationToStateMachine { get; private set; }
    public PlayerSkillsManager SkillManager { get; private set; }
    public PlayerStatsManager StatsManager { get; private set; }

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
    public PlayerRollState RollState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerSwordAttackState_01 SwordAttackState01 { get; private set; }
    public PlayerSwordAttackState_02 SwordAttackState02 { get; private set; }
    public PlayerSwordAttackState_03 SwordAttackState03 { get; private set; }
    public PlayerBowFireArrowShotState_Start FireArrowShotStateStart { get; private set; }
    public PlayerBowFireArrowShotState_Aim FireArrowShotStateAim { get; private set; }
    public PlayerBowFireArrowShotState_Finish FireArrowShotStateFinish { get; private set; }
    public PlayerOnRopeState_Aim OnRopeStateAim { get; private set; }
    public PlayerOnRopeState_Attach OnRopeStateAttach { get; private set; }
    public PlayerOnRopeState_Move OnRopeStateMove { get; private set; }
    public PlayerOnRopeState_Finish OnRopeStateFinish { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    #endregion

    #region Other Variables

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    #region Components

    public GameObject AliveGameObject { get; private set; }
    public Animator MyAnmator { get; private set; }
    public Rigidbody2D MyRigidbody { get; private set; }
    public BoxCollider2D MyBoxCollider2D { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public LineRenderer MyRopeLineRenderer;
    public Transform DashDirectionIndicator { get; private set; }
    public Transform RopeHingeAnchor { get; private set; }
    public Rigidbody2D RopeHingeAnchorRigidbody { get; private set; }
    public SpriteRenderer RopeHingeAnchorSpriteRenderer { get; private set; }
    public Transform Crosshair { get; private set; }
    public SpriteRenderer CrosshairSpriteRenderer { get; private set; }
    public DistanceJoint2D RopeJoint { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        FiniteStateMachine = new PlayerFiniteStateMachine();
        SkillManager = new PlayerSkillsManager();
        StatsManager = new PlayerStatsManager(_playerStatsData);

        IdleState = new PlayerIdleState(this, FiniteStateMachine, "idle", _idleStateData);
        MoveState = new PlayerMoveState(this, FiniteStateMachine, "move", _moveStateData);

        LandState = new PlayerLandState(this, FiniteStateMachine, "land");
        JumpState = new PlayerJumpState(this, FiniteStateMachine, "inAir", _jumpStateData);
        InAirState = new PlayerInAirState(this, FiniteStateMachine, "inAir", _inAirStateData);

        WallSlideState = new PlayerWallSlideState(this, FiniteStateMachine, "wallSlide", _wallSlideStateData);
        WallGrabState = new PlayerWallGrabState(this, FiniteStateMachine, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, FiniteStateMachine, "wallClimb", _wallClimbStateData);
        WallJumpState = new PlayerWallJumpState(this, FiniteStateMachine, "inAir", _wallJumpStateData);
        LedgeClimbState = new PlayerLedgeClimbState(this, FiniteStateMachine, "ledgeClimbState", _ledgeClimbStateData);

        DashState = new PlayerDashState(this, FiniteStateMachine, "inAir", _dashStateData);
        RollState = new PlayerRollState(this, FiniteStateMachine, "roll", _rollStateData);

        CrouchIdleState = new PlayerCrouchIdleState(this, FiniteStateMachine, "crouchIdle", _crouchIdleStateData);
        CrouchMoveState = new PlayerCrouchMoveState(this, FiniteStateMachine, "crouchMove", _crouchMoveStateData);

        OnRopeStateAim = new PlayerOnRopeState_Aim(this, FiniteStateMachine, "idle", _onRopeStateData);
        OnRopeStateAttach = new PlayerOnRopeState_Attach(this, FiniteStateMachine, "inAir", _onRopeStateData);
        OnRopeStateMove = new PlayerOnRopeState_Move(this, FiniteStateMachine, "inAir", _onRopeStateData);
        OnRopeStateFinish = new PlayerOnRopeState_Finish(this, FiniteStateMachine, "inAir", _onRopeStateData);

        SwordAttackState01 = new PlayerSwordAttackState_01(this, FiniteStateMachine, "swordAttack01", _swordAttackPosition01,
            _swordAttackStateData01);
        SwordAttackState02 = new PlayerSwordAttackState_02(this, FiniteStateMachine, "swordAttack02", _swordAttackPosition02,
            _swordAttackStateData02);
        SwordAttackState03 = new PlayerSwordAttackState_03(this, FiniteStateMachine, "swordAttack03", _swordAttackPosition03,
            _swordAttackStateData03);

        FireArrowShotStateStart = new PlayerBowFireArrowShotState_Start(this, FiniteStateMachine, "bowFireShotStart",
            _fireArrowShotAttackPosition, _fireArrowShotStateData);
        FireArrowShotStateAim = new PlayerBowFireArrowShotState_Aim(this, FiniteStateMachine, "bowFireShotAim",
            _fireArrowShotAttackPosition, _fireArrowShotStateData);
        FireArrowShotStateFinish = new PlayerBowFireArrowShotState_Finish(this, FiniteStateMachine, "bowFireShotFinish",
            _fireArrowShotAttackPosition, _fireArrowShotStateData);

        StunState = new PlayerStunState(this, FiniteStateMachine, "stun", StunStateData);
        // TODO: Create Dead animation
        DeadState = new PlayerDeadState(this, FiniteStateMachine, "stun", _deadStateData);
    }

    private void Start()
    {
        AliveGameObject = transform.Find("Alive Player").gameObject;

        AnimationToStateMachine = AliveGameObject.GetComponent<PlayerAnimationToStateMachine>();
        MyAnmator = AliveGameObject.GetComponent<Animator>();
        MyRigidbody = AliveGameObject.GetComponent<Rigidbody2D>();
        MyBoxCollider2D = AliveGameObject.GetComponent<BoxCollider2D>();
        MyRopeLineRenderer = AliveGameObject.GetComponent<LineRenderer>();
        RopeJoint = AliveGameObject.GetComponent<DistanceJoint2D>();

        DashDirectionIndicator = AliveGameObject.transform.Find("Dash Direction Indicator");

        RopeHingeAnchor = AliveGameObject.transform.Find("Rope Hinge Anchor");
        RopeHingeAnchorRigidbody = RopeHingeAnchor.GetComponent<Rigidbody2D>();
        RopeHingeAnchorSpriteRenderer = RopeHingeAnchor.GetComponent<SpriteRenderer>();

        Crosshair = AliveGameObject.transform.Find("Crosshair");
        CrosshairSpriteRenderer = Crosshair.GetComponent<SpriteRenderer>();

        InputHandler = GetComponent<PlayerInputHandler>();

        FacingDirection = 1;

        SkillManager.AddSkill(DashState);
        SkillManager.AddSkill(OnRopeStateAim);
        SkillManager.AddSkill(FireArrowShotStateStart);

        OnRopeStateFinish.ResetRope();

        _playerHealthBar.SetMaxHealth(_playerStatsData.maxHealth);

        AliveGameObject.transform.position = FindObjectOfType<PlayerCheckpointManager>().LastCheckpoint;

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

    public void Damage(AttackDetails attackDetails)
    {
        if (StatsManager.IsRolling)
        {
            return;
        }

        StatsManager.TakeDamage(attackDetails);

        Instantiate(_deadStateData.bloodEffectGO, AliveGameObject.transform.position, _deadStateData.bloodEffectGO.transform.rotation);

        if (StatsManager.IsDead)
        {
            FiniteStateMachine.ChangeCurrentState(DeadState);
        }
        else if (StatsManager.IsStunned && FiniteStateMachine.CurrentState != StunState)
        {
            FiniteStateMachine.ChangeCurrentState(StunState);
        }
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

    public void AddForce(Vector2 force, ForceMode2D mode)
    {
        MyRigidbody.AddForce(force, mode);
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(_groundCheck.position, _playerData.groundCheckRadius, _playerData.whatIsGround);

    public bool CheckIfTouchingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance,
        _playerData.whatIsGround);

    public bool CheckIfBackIsTouchingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _playerData.wallCheckDistance,
        _playerData.whatIsGround);

    public bool CheckIfTouchingLedge() => Physics2D.Raycast(_ledgeCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance,
        _playerData.whatIsGround);

    public bool CheckIfTouchingCeiling() => Physics2D.OverlapCircle(_ledgeCheck.position, _playerData.ceilingCheckRadius, _playerData.whatIsGround);

    public bool CheckIfCanStand(Vector2 position)
    {
        return Physics2D.Raycast(position + (Vector2.up * _ledgeClimbStateData.standCheckOffset) + (Vector2.right * FacingDirection *
            _ledgeClimbStateData.standCheckOffset), Vector2.up, _ledgeClimbStateData.standColliderHeight, _playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    private void Flip()
    {
        FacingDirection *= -1;
        AliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void SetBoxColliderHeight(float height)
    {
        Vector2 center = MyBoxCollider2D.offset;
        _workspace.Set(MyBoxCollider2D.size.x, height);

        center.y += (height - MyBoxCollider2D.size.y) / 2;

        MyBoxCollider2D.size = _workspace;
        MyBoxCollider2D.offset = center;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _playerData.wallCheckDistance, _playerData.whatIsGround);
        float xDistance = xHit.distance;

        _workspace.Set((xDistance + _ledgeClimbStateData.cornerTolerance) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)_workspace, Vector2.down, _ledgeCheck.position.y - _wallCheck.position.y
            + _ledgeClimbStateData.cornerTolerance, _playerData.whatIsGround);
        float yDistance = yHit.distance;

        _workspace.Set(_wallCheck.position.x + (xDistance * FacingDirection), _ledgeCheck.position.y - yDistance);

        return _workspace;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_swordAttackPosition01.position, _swordAttackData01.attackRadius);
        //Gizmos.DrawWireSphere(_swordAttackPosition02.position, _swordAttackData02.attackRadius);
        //Gizmos.DrawWireSphere(_swordAttackPosition03.position, _swordAttackData03.attackRadius);

        Gizmos.DrawWireSphere(_ceilingCheck.position, _playerData.ceilingCheckRadius);
    }

    #endregion
}
