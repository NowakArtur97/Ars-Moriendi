using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private D_PlayerDeadState _deadStateData;

    public PlayerDeadState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerDeadState deadStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _deadStateData = deadStateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_deadStateData.deathChunkEffectGO, Player.AliveGameObject.transform.position, _deadStateData.deathChunkEffectGO.transform.rotation);
        GameObject.Instantiate(_deadStateData.bloodEffectGO, Player.AliveGameObject.transform.position, _deadStateData.bloodEffectGO.transform.rotation);

        GameObject.Destroy(Player);
    }
}
