using UnityEngine;

public class Boar : Entity
{
    [SerializeField] private D_MoveState stateData;

    public Boar_MoveState moveState { get; private set; }

    protected override void Start()
    {
        base.Start();

        moveState = new Boar_MoveState(finiteStateMachine, this, "move", stateData, this);
    }
}
