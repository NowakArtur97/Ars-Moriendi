using UnityEngine;

public class Slime : Enemy
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_JumpingMoveState _jumpingMoveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_AreaAttackState _areaAttackStateData;
    [SerializeField] private D_StunState _stunStateData;
    [SerializeField] private D_DeadState _deadStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;
    [SerializeField] private Transform _areaAttackPosition;

    public Slime_IdleState IdleState { get; private set; }
    public Slime_JumpingMoveState JumpingMoveState { get; private set; }
    public Slime_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Slime_MeleeAttackState MeleeAttackState { get; private set; }
    public Slime_AreaAttackState AreaAttackState { get; private set; }
    public Slime_StunState StunState { get; private set; }
    public Slime_DeadState DeadState { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Slime_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        JumpingMoveState = new Slime_JumpingMoveState(FiniteStateMachine, this, "jumpingMove", _jumpingMoveStateData, this);
        // TODO: SLIME Create player detected animation
        PlayerDetectedState = new Slime_PlayerDetectedState(FiniteStateMachine, this, "idle", _playerDetectedStateData, this);
        MeleeAttackState = new Slime_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        // TODO: SLIME Create player area attack animation
        AreaAttackState = new Slime_AreaAttackState(FiniteStateMachine, this, "areaAttack", _areaAttackPosition, _areaAttackStateData, this);
        StunState = new Slime_StunState(FiniteStateMachine, this, "stun", _stunStateData, this);
        DeadState = new Slime_DeadState(FiniteStateMachine, this, "dead", _deadStateData, this);

        FiniteStateMachine.Initialize(IdleState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (StatsManager.IsDead)
        {
            FiniteStateMachine.ChangeState(DeadState);
        }
        else if (StatsManager.IsStunned && FiniteStateMachine.currentState != StunState)
        {
            FiniteStateMachine.ChangeState(StunState);
        }
        else
        {
            FiniteStateMachine.ChangeState(JumpingMoveState);
        }

        foreach (GameObject effect in _deadStateData.damageEffects)
        {
            GameObject.Instantiate(effect, AliveGameObject.transform.position, effect.transform.rotation);
        }
    }
}
