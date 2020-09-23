using UnityEngine;

public class GoblinArcher : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public GoblinArcher_IdleState idleState { get; private set; }
    public GoblinArcher_MoveState moveState { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        idleState = new GoblinArcher_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new GoblinArcher_MoveState(finiteStateMachine, this, "move", moveStateData, this);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
