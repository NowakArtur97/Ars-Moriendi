using UnityEngine;

public class DamageState : EnemyState
{
    protected D_DamageState StateData;

    protected bool IsPlayerInMaxAgroRange;

    public DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocityY(StateData.damageHopSpeed);

        foreach (GameObject hitPartcile in StateData.hitPartciles)
        {
            GameObject.Instantiate(hitPartcile, Enemy.AliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        }
    }

    public override void Exit()
    {
        base.Exit();

        Enemy.SetVelocityY(StateData.afterHopSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAnimationFinished = true;
    }
}
