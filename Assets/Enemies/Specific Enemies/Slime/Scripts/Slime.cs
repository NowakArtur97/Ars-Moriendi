using UnityEngine;

public class Slime : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_JumpingMoveState _jumpingMoveStateData;

    public Slime_IdleState IdleState { get; private set; }
    public Slime_JumpingMoveState JumpingMoveState { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Slime_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        JumpingMoveState = new Slime_JumpingMoveState(FiniteStateMachine, this, "jumpingMove", _jumpingMoveStateData, this);

        FiniteStateMachine.Initialize(JumpingMoveState);
    }
}
