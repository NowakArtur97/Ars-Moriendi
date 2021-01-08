using UnityEngine;

public class GoblinArcher : Enemy
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_MoveState _moveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_StunState _stunStateData;
    [SerializeField] private D_DeadState _deadStateData;
    [SerializeField] public D_DodgeState _dodgeStateData;
    [SerializeField] public D_RangedAttackState _rangedAttackStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;
    [SerializeField] private Transform _rangedAttackPosition;

    public GoblinArcher_IdleState IdleState { get; private set; }
    public GoblinArcher_MoveState MoveState { get; private set; }
    public GoblinArcher_PlayerDetectedState PlayerDetectedState { get; private set; }
    public GoblinArcher_LookForPlayerState LookForPlayerState { get; private set; }
    public GoblinArcher_MeleeAttackState MeleeAttackState { get; private set; }
    public GoblinArcher_StunState StunState { get; private set; }
    public GoblinArcher_DeadState DeadState { get; private set; }
    public GoblinArcher_DodgeState DodgeState { get; private set; }
    public GoblinArcher_RangedAttackState RangedAttackState { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new GoblinArcher_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        MoveState = new GoblinArcher_MoveState(FiniteStateMachine, this, "move", _moveStateData, this);
        PlayerDetectedState = new GoblinArcher_PlayerDetectedState(FiniteStateMachine, this, "playerDetected", _playerDetectedStateData, this);
        LookForPlayerState = new GoblinArcher_LookForPlayerState(FiniteStateMachine, this, "lookForPlayer", _lookForPlayerStateData, this);
        MeleeAttackState = new GoblinArcher_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        DodgeState = new GoblinArcher_DodgeState(FiniteStateMachine, this, "dodge", _dodgeStateData, this);
        RangedAttackState = new GoblinArcher_RangedAttackState(FiniteStateMachine, this, "rangedAttack", _rangedAttackPosition, _rangedAttackStateData, this);
        StunState = new GoblinArcher_StunState(FiniteStateMachine, this, "stun", _stunStateData, this);
        // GOBLIN ARCHER Create death animation
        DeadState = new GoblinArcher_DeadState(FiniteStateMachine, this, "dead", _deadStateData, this);

        FiniteStateMachine.Initialize(MoveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (IsDead)
        {
            FiniteStateMachine.ChangeState(DeadState);
        }
        else if (IsStunned && FiniteStateMachine.currentState != StunState)
        {
            FiniteStateMachine.ChangeState(StunState);
        }
        else if (CheckIfPlayerInLongRangeAction())
        {
            FiniteStateMachine.ChangeState(PlayerDetectedState);
        }
        else
        {
            LookForPlayerState.SetShouldTurnImmediately(true);
            FiniteStateMachine.ChangeState(LookForPlayerState);
        }

        foreach (GameObject effect in _deadStateData.damageEffects)
        {
            GameObject.Instantiate(effect, AliveGameObject.transform.position, effect.transform.rotation);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackRadius);
    }
}
