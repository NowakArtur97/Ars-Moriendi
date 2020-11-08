using UnityEngine;

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
    [SerializeField] public D_DodgeState dodgeStateData;
    [SerializeField] public D_RangedAttackState rangedAttackStateData;

    [Header("Attack Positions")]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangedAttackPosition;

    public GoblinArcher_IdleState idleState { get; private set; }
    public GoblinArcher_MoveState moveState { get; private set; }
    public GoblinArcher_PlayerDetectedState playerDetectedState { get; private set; }
    public GoblinArcher_LookForPlayerState lookForPlayerState { get; private set; }
    public GoblinArcher_MeleeAttackState meleeAttackState { get; private set; }
    public GoblinArcher_StunState stunState { get; private set; }
    public GoblinArcher_DeadState deadState { get; private set; }
    public GoblinArcher_DodgeState dodgeState { get; private set; }
    public GoblinArcher_RangedAttackState rangedAttackState { get; private set; }

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
        rangedAttackState = new GoblinArcher_RangedAttackState(finiteStateMachine, this, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        finiteStateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        GameObject.Instantiate(deadStateData.bloodEffectGO, aliveGameObject.transform.position, deadStateData.bloodEffectGO.transform.rotation);

        if (isDead)
        {
            finiteStateMachine.ChangeState(deadState);
        }
        else if (isStunned && finiteStateMachine.currentState != stunState)
        {
            finiteStateMachine.ChangeState(stunState);
        }
        else if (CheckIfPlayerInCloseRangeAction())
        {
            finiteStateMachine.ChangeState(meleeAttackState);
        }
        else if (CheckIfPlayerInLongRangeAction())
        {
            finiteStateMachine.ChangeState(rangedAttackState);
        }
        else
        {
            lookForPlayerState.SetShouldTurnImmediately(true);
            finiteStateMachine.ChangeState(lookForPlayerState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
