using UnityEngine;

public class Boar : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_SlowDownState slowDownStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform meleeAttackPosition;

    public Boar_IdleState idleState;
    public Boar_MoveState moveState;
    public Boar_PlayerDetectedState playerDetectedState;
    public Boar_ChargeState chargeState;
    public Boar_LookForPlayerState lookForPlayerState;
    public Boar_SlowDownState slowDownState;
    public Boar_MeleeAttackState meleeAttackState;

    protected override void Start()
    {
        base.Start();

        idleState = new Boar_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new Boar_MoveState(finiteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new Boar_PlayerDetectedState(finiteStateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Boar_ChargeState(finiteStateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Boar_LookForPlayerState(finiteStateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        slowDownState = new Boar_SlowDownState(finiteStateMachine, this, "slowDown", slowDownStateData, this);
        meleeAttackState = new Boar_MeleeAttackState(finiteStateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

        finiteStateMachine.Initialize(moveState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
