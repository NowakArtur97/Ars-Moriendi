using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Checks")]
    [SerializeField] protected Transform WallCheck;
    [SerializeField] protected Transform LedgeCheck;
    [SerializeField] protected Transform PlayerCheck;
    [SerializeField] protected Transform PlayerJumpedOverCheck;
    [SerializeField] protected Transform GroundCheck;

    public D_Entity EntityData;

    public FiniteStateMachine FiniteStateMachine;
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }

    public Rigidbody2D MyRigidbody2D { get; private set; }
    public Animator MyAnimator { get; private set; }
    public GameObject AliveGameObject { get; private set; }

    public float FacingDirection { get; private set; }
    private float _currentHealth;

    protected bool IsDead;

    private float _currentStunResistance;
    private float _lastDamageTime;
    public int LastDamageDirection { get; private set; }

    protected bool IsStunned;

    private Vector2 _velocityWorkSpace;

    protected virtual void Start()
    {
        AliveGameObject = transform.Find("Alive").gameObject;
        MyRigidbody2D = AliveGameObject.GetComponent<Rigidbody2D>();
        MyAnimator = AliveGameObject.GetComponent<Animator>();

        AnimationToStateMachine = AliveGameObject.GetComponent<AnimationToStateMachine>();

        FiniteStateMachine = new FiniteStateMachine();

        FacingDirection = 1;
        _currentHealth = EntityData.maxHealth;

        _currentStunResistance = EntityData.stunResistance;
    }

    protected virtual void Update()
    {
        FiniteStateMachine.currentState.LogicUpdate();

        if (Time.time >= _lastDamageTime + EntityData.stunRecorveryTime)
        {
            ResetStunResistance();
        }

        MyAnimator.SetFloat("yVelocity", MyRigidbody2D.velocity.y);
    }

    protected virtual void FixedUpdate()
    {
        FiniteStateMachine.currentState.PhysicsUpdate();
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkSpace.Set(MyRigidbody2D.velocity.x, velocity);

        MyRigidbody2D.velocity = _velocityWorkSpace;
    }

    public virtual void ResetStunResistance()
    {
        IsStunned = false;
        _currentStunResistance = EntityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        _currentHealth -= attackDetails.damageAmmount;
        _currentStunResistance -= attackDetails.stunDamageAmount;

        _lastDamageTime = Time.time;

        DamageHop(EntityData.damageHopSpeed);

        LastDamageDirection = attackDetails.position.x > AliveGameObject.transform.position.x ? -1 : 1;

        foreach (GameObject hitPartcile in EntityData.hitPartciles)
        {
            Instantiate(hitPartcile, AliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        }

        if (_currentStunResistance <= 0)
        {
            IsStunned = true;
        }

        if (_currentHealth <= 0)
        {
            IsDead = true;
        }
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

    public virtual bool CheckWall() => Physics2D.Raycast(WallCheck.position, AliveGameObject.transform.right, EntityData.wallCheckDistance, EntityData.whatIsGround);

    public virtual bool CheckLedge() => Physics2D.Raycast(LedgeCheck.position, Vector2.down, EntityData.ledgeCheckDistance, EntityData.whatIsGround);

    public virtual bool CheckGround => Physics2D.OverlapCircle(GroundCheck.position, EntityData.groundCheckRadius, EntityData.whatIsGround);

    public virtual bool CheckIfPlayerInMinAgro => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right, EntityData.minAgroDistance,
        EntityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInMaxAgro => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right, EntityData.maxAgroDistance,
        EntityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInCloseRangeAction() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right,
        EntityData.closeRangeActionDistance, EntityData.whatIsPlayer);

    public virtual bool CheckIfPlayerInLongRangeAction() => Physics2D.Raycast(PlayerCheck.position, AliveGameObject.transform.right,
        EntityData.closeRangeActionDistance, EntityData.whatIsPlayer);

    public virtual bool CheckIfPlayerJumpedOver() => Physics2D.Raycast(PlayerJumpedOverCheck.position, Vector2.up, EntityData.maxPlayerJumpedOverDistance,
        EntityData.whatIsPlayer);

    public void Flip()
    {
        FacingDirection *= -1;

        AliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(WallCheck.position, new Vector2(WallCheck.position.x - EntityData.wallCheckDistance, WallCheck.position.y));
        Gizmos.DrawLine(LedgeCheck.position, new Vector2(LedgeCheck.position.x, LedgeCheck.position.y - EntityData.ledgeCheckDistance));
        Gizmos.DrawLine(PlayerJumpedOverCheck.position, new Vector2(PlayerJumpedOverCheck.position.x,
            PlayerJumpedOverCheck.position.y + EntityData.maxPlayerJumpedOverDistance));

        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * EntityData.closeRangeActionDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * EntityData.longRangeActionDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * EntityData.minAgroDistance)), 0.2f);
        Gizmos.DrawWireSphere((PlayerCheck.position + (Vector3)(Vector2.right * EntityData.maxAgroDistance)), 0.2f);
    }
}
