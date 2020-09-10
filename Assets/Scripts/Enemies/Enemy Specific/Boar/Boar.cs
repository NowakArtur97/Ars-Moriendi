using UnityEngine;

public class Boar : Entity
{
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;

    public Boar_IdleState idleState;
    public Boar_MoveState moveState;
    public Boar_PlayerDetectedState playerDetectedState;
    public Boar_ChargeState chargeState;
    public Boar_LookForPlayerState lookForPlayerState;

    protected override void Start()
    {
        base.Start();

        idleState = new Boar_IdleState(finiteStateMachine, this, "idle", idleStateData, this);
        moveState = new Boar_MoveState(finiteStateMachine, this, "move", moveStateData, this);
        playerDetectedState = new Boar_PlayerDetectedState(finiteStateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Boar_ChargeState(finiteStateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Boar_LookForPlayerState(finiteStateMachine, this, "lookForPlayer", lookForPlayerStateData, this);

        finiteStateMachine.Initialize(moveState);
    }
}
