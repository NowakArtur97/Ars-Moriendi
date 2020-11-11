public class PlayerMoveOnRopeState : PlayerAbilityState
{
    private int _xInput;

    public PlayerMoveOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;

        if (!IsExitingState)
        {
            Player.CheckIfShouldFlip(_xInput);
            Player.SetVelocityX(PlayerData.movementVelocity * _xInput);

            //if (_xInput == 0)
            //{
            //    FiniteStateMachine.ChangeState(Player.);
            //}
        }
    }
}
