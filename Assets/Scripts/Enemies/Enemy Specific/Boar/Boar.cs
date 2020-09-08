using UnityEngine;

public class Boar : Entity
{
    public Boar_IdleState idleState;
    public Boar_MoveState moveState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Boar_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new Boar_MoveState(finiteStateMachine, this, "move", moveStateData, this);

        finiteStateMachine.Initialize(moveState);
    }
}
