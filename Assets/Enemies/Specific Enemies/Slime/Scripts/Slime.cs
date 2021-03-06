﻿using UnityEngine;

public class Slime : Enemy
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_JumpingMoveState _jumpingMoveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_AreaAttackState _areaAttackStateData;
    [SerializeField] private D_DamageState _damageStateData;
    [SerializeField] private D_StunState _stunStateData;
    [SerializeField] private D_DeadState _deadStateData;

    [Header("Effects Data")]
    [SerializeField] public D_DissolveEffect DissolveEffectData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;
    [SerializeField] private Transform _areaAttackPosition;

    public Slime_IdleState IdleState { get; private set; }
    public Slime_JumpingMoveState JumpingMoveState { get; private set; }
    public Slime_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Slime_MeleeAttackState MeleeAttackState { get; private set; }
    public Slime_AreaAttackState AreaAttackState { get; private set; }
    public Slime_DamageState DamageState { get; private set; }
    public Slime_StunState StunState { get; private set; }
    public Slime_DeadState DeadState { get; private set; }

    public DissolveEffect DissolveEffect { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Slime_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        JumpingMoveState = new Slime_JumpingMoveState(FiniteStateMachine, this, "jumpingMove", _jumpingMoveStateData, this);
        PlayerDetectedState = new Slime_PlayerDetectedState(FiniteStateMachine, this, "playerDetected", _playerDetectedStateData, this);
        MeleeAttackState = new Slime_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        AreaAttackState = new Slime_AreaAttackState(FiniteStateMachine, this, "areaAttack", _areaAttackPosition, _areaAttackStateData, this);
        DamageState = new Slime_DamageState(FiniteStateMachine, this, "damage", _damageStateData, this);
        StunState = new Slime_StunState(FiniteStateMachine, this, "stun", _stunStateData, this);
        DeadState = new Slime_DeadState(FiniteStateMachine, this, "dead", _deadStateData, this);

        FiniteStateMachine.Initialize(IdleState);

        DissolveEffect = new DissolveEffect();
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
}
