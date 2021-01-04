using UnityEngine;

public class PlayerOnRopeState_Aim : PlayerOnRopeState
{
    private Vector2 _ropeDirectionInput;
    private Vector3 _crossHairPosition;
    private float _aimAngle;
    private Vector2 _aimDirection;

    public PlayerOnRopeState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, onRopeStateData)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (RopeInputStop)
            {
                Player.InputHandler.UseSecondaryAttackInputStop();

                Player.OnRopeStateAttach.SetAimDirection(_aimDirection);

                Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateAttach);
            }
            else
            {
                _ropeDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
                _aimAngle = Mathf.Atan2(_ropeDirectionInput.y, _ropeDirectionInput.x);

                SetCrosshairPosition();
            }
        }
    }

    private void SetCrosshairPosition()
    {
        if (_aimAngle < 0f)
        {
            _aimAngle = Mathf.PI * 2 + _aimAngle;
        }
        _aimDirection = Quaternion.Euler(0, 0, _aimAngle * Mathf.Rad2Deg) * Vector2.right;

        if (!Player.Crosshair.gameObject.activeInHierarchy)
        {
            Player.Crosshair.gameObject.SetActive(true);
        }

        Player.RopeHingeAnchor.gameObject.SetActive(false);

        PlayerPosition = Player.AliveGameObject.transform.position;
        float x = PlayerPosition.x + OnRopeStateData.ropeCrosshairOffset * Mathf.Cos(_aimAngle);
        float y = PlayerPosition.y + OnRopeStateData.ropeCrosshairOffset * Mathf.Sin(_aimAngle);

        _crossHairPosition = new Vector3(x, y, 0);
        Player.Crosshair.position = _crossHairPosition;
    }

    public override bool CanUseAbility() => !Player.CheckIfTouchingCeiling();
}
