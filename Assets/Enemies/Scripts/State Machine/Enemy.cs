using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Transform Checks")]
    [SerializeField] protected Transform WallCheck;
    [SerializeField] protected Transform WallBehindCheck;
    [SerializeField] protected Transform LedgeCheck;
    [SerializeField] protected Transform PlayerCheck;
    [SerializeField] protected Transform PlayerJumpedOverCheck;
    [SerializeField] protected Transform GroundCheck;

    [Header("Base Data")]
    [SerializeField] private D_EnemyBase _entityData;
    [SerializeField] private D_EnemyStats _enemyStatsData;

    public FiniteStateMachine FiniteStateMachine { get; private set; }
    public EnemyStatsManager StatsManager { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public AttackAnimationToStateMachine AttackAnimationToStateMachine { get; private set; }

    public Rigidbody2D MyRigidbody2D { get; private set; }
    public Animator MyAnimator { get; private set; }
    public GameObject AliveGameObject { get; private set; }

    public float FacingDirection { get; private set; }
    public int LastDamageDirection { get; private set; }

    private Vector2 _velocityWorkSpace;
    private HealthBar _healthBar;

    protected virtual void Awake()
    {
        FiniteStateMachine = new FiniteStateMachine();
        StatsManager = new EnemyStatsManager(_enemyStatsData);

        FacingDirection = 1;
    }

    protected virtual void Start()
    {
        AliveGameObject = transform.Find("Alive").gameObject;
        MyRigidbody2D = AliveGameObject.GetComponent<Rigidbody2D>();
        MyAnimator = AliveGameObject.GetComponent<Animator>();

        AnimationToStateMachine = AliveGameObject.GetComponent<AnimationToStateMachine>();
        AttackAnimationToStateMachine = AliveGameObject.GetComponent<AttackAnimationToStateMachine>();

        _healthBar = AliveGameObject.GetComponentInChildren<HealthBar>();
        _healthBar.SetMaxHealth(_enemyStatsData.maxHealth);
    }

    protected virtual void Update()
    {
        FiniteStateMachine.CurrentState.LogicUpdate();

        MyAnimator.SetFloat("yVelocity", MyRigidbody2D.velocity.y);
    }

    protected virtual void FixedUpdate()
    {
        FiniteStateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        StatsManager.TakeDamage(attackDetails);

        LastDamageDirection = attackDetails.position.x > AliveGameObject.transform.position.x ? -1 : 1;
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkSpace.Set(velocity * FacingDirection, MyRigidbody2D.velocity.y);

        MyRigidbody2D.velocity = _velocityWorkSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, float direction)
    {
        angle.Normalize();

        _velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);

        MyRigidbody2D.velocity = _velocityWorkSpace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        _velocityWorkSpace.Set(MyRigidbody2D.velocity.x, velocity);

        MyRigidbody2D.velocity = _velocityWorkSpace;
    }

    public virtual bool CheckIfTouchingWall() => Physics2D.Raycast(WallCheck.position, AliveGameObject.transform.right, _entityData.wallCheckDistance, _entityData.whatIsGround);

    public virtual bool CheckIfBackIsTouchingWall() => Physics2D.Raycast(WallBehindCheck.position, -AliveGameObject.transform.right,
        _entityData.wallBehindCheckDistance, _entityData.whatIsGround);

    public virtual bool CheckIfTouchingLedge() => Physics2D.Raycast(LedgeCheck.position, Vector2.down, _entityData.ledgeCheckDistance, _entityData.whatIsGround);

    public virtual bool CheckIfGrounded() => Physics2D.OverlapCircle(GroundCheck.position, _entityData.groundCheckRadius, _entityData.whatIsGround);

    public virtual bool CheckIfPlayerInMinAgro() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right, _entityData.minAgroDistance,
        _entityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInMaxAgro() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right, _entityData.maxAgroDistance,
        _entityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInCloseRangeAction() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right,
        _entityData.closeRangeActionDistance, _entityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInLongRangeAction() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right,
        _entityData.closeRangeActionDistance, _entityData.whatIsPlayer);

    public virtual bool CheckIfPlayerJumpedOver() => Physics2D.Raycast(PlayerJumpedOverCheck.position, Vector2.up, _entityData.maxPlayerJumpedOverDistance,
        _entityData.whatIsPlayer);

    public void Flip()
    {
        FacingDirection *= -1;

        AliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(WallCheck.position, new Vector2(WallCheck.position.x - _entityData.wallCheckDistance, WallCheck.position.y));
        Gizmos.DrawLine(WallBehindCheck.position, new Vector2(WallBehindCheck.position.x - _entityData.wallBehindCheckDistance, WallBehindCheck.position.y));
        Gizmos.DrawLine(LedgeCheck.position, new Vector2(LedgeCheck.position.x, LedgeCheck.position.y - _entityData.ledgeCheckDistance));
        Gizmos.DrawLine(PlayerJumpedOverCheck.position, new Vector2(PlayerJumpedOverCheck.position.x,
            PlayerJumpedOverCheck.position.y + _entityData.maxPlayerJumpedOverDistance));

        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * _entityData.closeRangeActionDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * _entityData.longRangeActionDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * _entityData.minAgroDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * _entityData.maxAgroDistance)), 0.2f);
    }
}
