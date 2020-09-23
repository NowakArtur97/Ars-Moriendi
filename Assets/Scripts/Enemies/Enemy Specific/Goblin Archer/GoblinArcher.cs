using UnityEngine;

public class GoblinArcher : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;

    public GoblinArcher_IdleState idleState { get; private set; }
    public GoblinArcher_MoveState moveState { get; private set; }
    public GoblinArcher_PlayerDetectedState playerDetectedState { get; private set; }

    protected override void Start()
    {
        base.Start();

        idleState = new GoblinArcher_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new GoblinArcher_MoveState(finiteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new GoblinArcher_PlayerDetectedState(finiteStateMachine, this, "playerDetected", playerDetectedStateData, this);

        finiteStateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }
}
