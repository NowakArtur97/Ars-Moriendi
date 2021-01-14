using UnityEngine;

public class Boar : Enemy
{
    [Header("States Data")]

    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_MoveState _moveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_ChargeState _chargeStateData;
    [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] private D_SlowDownState _slowDownStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_DamageState _damageStateData;
    [SerializeField] private D_StunState _stunStateData;
    [SerializeField] private D_DeadState _deadStateData;

    [Header("Effects Data")]
    [SerializeField] public D_DissolveEffect DissolveEffectData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;

    public Boar_IdleState IdleState { get; private set; }
    public Boar_MoveState MoveState { get; private set; }
    public Boar_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Boar_ChargeState ChargeState { get; private set; }
    public Boar_LookForPlayerState LookForPlayerState { get; private set; }
    public Boar_SlowDownState SlowDownState { get; private set; }
    public Boar_MeleeAttackState MeleeAttackState { get; private set; }
    public Boar_DamageState DamageState { get; private set; }
    public Boar_StunState StunState { get; private set; }
    public Boar_DeadState DeadState { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Boar_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        MoveState = new Boar_MoveState(FiniteStateMachine, this, "move", _moveStateData, this);
        PlayerDetectedState = new Boar_PlayerDetectedState(FiniteStateMachine, this, "playerDetected", _playerDetectedStateData, this);
        ChargeState = new Boar_ChargeState(FiniteStateMachine, this, "charge", _chargeStateData, this);
        LookForPlayerState = new Boar_LookForPlayerState(FiniteStateMachine, this, "lookForPlayer", _lookForPlayerStateData, this);
        SlowDownState = new Boar_SlowDownState(FiniteStateMachine, this, "slowDown", _slowDownStateData, this);
        MeleeAttackState = new Boar_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        DamageState = new Boar_DamageState(FiniteStateMachine, this, "damage", _damageStateData, this);
        StunState = new Boar_StunState(FiniteStateMachine, this, "stun", _stunStateData, this);
        DeadState = new Boar_DeadState(FiniteStateMachine, this, "dead", _deadStateData, this);

        FiniteStateMachine.Initialize(MoveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        if (DeadState.IsDead)
        {
            return;
        }

        bool canDamage = Time.time >= StatsManager.LastDamageTime + _damageStateData.timeBeforeNextDamage;

        base.Damage(attackDetails);

        foreach (GameObject hitPartcile in _damageStateData.hitPartciles)
        {
            GameObject.Instantiate(hitPartcile, AliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        }

        if (StatsManager.IsDead)
        {
            FiniteStateMachine.ChangeState(DeadState);
        }
        else if (canDamage)
        {
            FiniteStateMachine.ChangeState(DamageState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackRadius);
    }
}
