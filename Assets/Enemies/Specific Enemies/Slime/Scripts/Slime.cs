using UnityEngine;

public class Slime : Enemy
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_JumpingMoveState _jumpingMoveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_AreaAttackState _areaAttackStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;
    [SerializeField] private Transform _areaAttackPosition;

    public Slime_IdleState IdleState { get; private set; }
    public Slime_JumpingMoveState JumpingMoveState { get; private set; }
    public Slime_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Slime_MeleeAttackState MeleeAttackState { get; private set; }
    public Slime_AreaAttackState AreaAttackState { get; private set; }

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

        FiniteStateMachine.Initialize(IdleState);
    }
}
