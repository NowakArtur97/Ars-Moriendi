using UnityEngine;

public class Boar : Entity
{
    public Boar_IdleState idleState;
    public Boar_MoveState moveState;
    public Boar_PlayerDetectedState playerDetectedState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Boar_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new Boar_MoveState(finiteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new Boar_PlayerDetectedState(finiteStateMachine, this, "playerDetected", playerDetectedStateData, this);

        finiteStateMachine.Initialize(moveState);
    }
}
