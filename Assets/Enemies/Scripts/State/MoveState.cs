public abstract class MoveState : EnemyState
{
    protected D_MoveState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public MoveState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_MoveState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(StateData.movementSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Enemy.CheckIfTouchingWall();
        IsDetectingLedge = Enemy.CheckIfTouchingLedge();
        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }
}
