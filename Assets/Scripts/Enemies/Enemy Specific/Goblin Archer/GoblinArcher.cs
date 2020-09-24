﻿using UnityEngine;

public class GoblinArcher : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_DodgeState dodgeStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform meleeAttackPosition;

    public GoblinArcher_IdleState idleState { get; private set; }
    public GoblinArcher_MoveState moveState { get; private set; }
    public GoblinArcher_PlayerDetectedState playerDetectedState { get; private set; }
    public GoblinArcher_LookForPlayerState lookForPlayerState { get; private set; }
    public GoblinArcher_MeleeAttackState meleeAttackState { get; private set; }
    public GoblinArcher_StunState stunState { get; private set; }
    public GoblinArcher_DeadState deadState { get; private set; }
    public GoblinArcher_DodgeState dodgeState { get; private set; }

    protected override void Start()
    {
        base.Start();

        idleState = new GoblinArcher_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new GoblinArcher_MoveState(finiteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new GoblinArcher_PlayerDetectedState(finiteStateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new GoblinArcher_LookForPlayerState(finiteStateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new GoblinArcher_MeleeAttackState(finiteStateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new GoblinArcher_StunState(finiteStateMachine, this, "stun", stunStateData, this);
        deadState = new GoblinArcher_DeadState(finiteStateMachine, this, "dead", deadStateData, this);
        dodgeState = new GoblinArcher_DodgeState(finiteStateMachine, this, "dodge", dodgeStateData, this);

        finiteStateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            finiteStateMachine.ChangeState(deadState);
        }
        else if (isStunned && finiteStateMachine.currentState != stunState)
        {
            finiteStateMachine.ChangeState(stunState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}