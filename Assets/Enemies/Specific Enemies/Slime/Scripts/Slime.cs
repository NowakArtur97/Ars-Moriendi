using UnityEngine;

public class Slime : Entity
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_MoveState _moveStateData;

    public Slime_IdleState IdleState { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Slime_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);

        FiniteStateMachine.Initialize(IdleState);
    }
}
