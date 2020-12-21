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
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform meleeAttackPosition;

    public Boar_IdleState idleState { get; private set; }
    public Boar_MoveState moveState { get; private set; }
    public Boar_PlayerDetectedState playerDetectedState { get; private set; }
    public Boar_ChargeState chargeState { get; private set; }
    public Boar_LookForPlayerState lookForPlayerState { get; private set; }
    public Boar_SlowDownState slowDownState { get; private set; }
    public Boar_MeleeAttackState meleeAttackState { get; private set; }
    public Boar_StunState stunState { get; private set; }
    public Boar_DeadState deadState { get; private set; }

    protected override void Start()
    {
        base.Start();

        idleState = new Boar_IdleState(FiniteStateMachine, this, "idle", idleStateData, this);
        moveState = new Boar_MoveState(FiniteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new Boar_PlayerDetectedState(FiniteStateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Boar_ChargeState(FiniteStateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Boar_LookForPlayerState(FiniteStateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        slowDownState = new Boar_SlowDownState(FiniteStateMachine, this, "slowDown", slowDownStateData, this);
        meleeAttackState = new Boar_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Boar_StunState(FiniteStateMachine, this, "stun", stunStateData, this);
        deadState = new Boar_DeadState(FiniteStateMachine, this, "dead", deadStateData, this);

        FiniteStateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        GameObject.Instantiate(deadStateData.bloodEffectGO, AliveGameObject.transform.position, deadStateData.bloodEffectGO.transform.rotation);

        if (IsDead)
        {
            FiniteStateMachine.ChangeState(deadState);
        }
        else if (IsStunned && FiniteStateMachine.currentState != stunState)
        {
            FiniteStateMachine.ChangeState(stunState);
        }
        else if (CheckIfPlayerInCloseRangeAction())
        {
            FiniteStateMachine.ChangeState(meleeAttackState);
        }
        else
        {
            lookForPlayerState.SetShouldTurnImmediately(true);
            FiniteStateMachine.ChangeState(lookForPlayerState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
