using UnityEngine;

public class PlayerMoveOnRopeState : PlayerAbilityState
{
    private int _xInput;
    private Vector2 _ropeDirectionInput;
    private float _aimAngle;
    private Vector2 _aimDirection;
    private bool _ropeInputStop;
    private Vector2 _playerPosition;
    private Vector3 _crossHairPosition;
    private bool _ropeAttached;

    public PlayerMoveOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _playerPosition = Player.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            _xInput = Player.InputHandler.NormalizedInputX;
            _ropeDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
            _ropeInputStop = Player.InputHandler.SecondaryAttackInputStop;
            _aimAngle = Mathf.Atan2(_ropeDirectionInput.y, _ropeDirectionInput.x);

            if (_aimAngle < 0f)
            {
                _aimAngle = Mathf.PI * 2 + _aimAngle;
            }
            _aimDirection = Quaternion.Euler(0, 0, _aimAngle * Mathf.Rad2Deg) * Vector2.right;
            _playerPosition = Player.transform.position;

            if (_ropeInputStop)
            {
                Player.Crosshair.gameObject.SetActive(false);
                IsAbilityDone = true;
            }
            else if (!_ropeAttached)
            {
                SetCrosshairPosition();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        _ropeAttached = false;
    }

    private void SetCrosshairPosition()
    {
        if (!Player.Crosshair.gameObject.activeInHierarchy)
        {
            Player.Crosshair.gameObject.SetActive(true);
        }

        float x = Player.transform.position.x + PlayerData.ropeCrosshairOffset * Mathf.Cos(_aimAngle);
        float y = Player.transform.position.y + PlayerData.ropeCrosshairOffset * Mathf.Sin(_aimAngle);

        _crossHairPosition = new Vector3(x, y, 0);
        Player.Crosshair.position = _crossHairPosition;
    }
}
