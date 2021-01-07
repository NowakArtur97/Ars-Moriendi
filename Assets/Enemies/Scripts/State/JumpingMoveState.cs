public class JumpingMoveState : EnemyState
{
    protected D_JumpingMoveState StateData;

    protected bool IsJumpOver;

    public JumpingMoveState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_JumpingMoveState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(StateData.jumpingMovementSpeed, StateData.jumpingAngle, Enemy.FacingDirection);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Enemy.SetVelocity(0.0f);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAnimationFinished = true;
    }
}
