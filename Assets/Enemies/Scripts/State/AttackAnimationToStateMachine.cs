using UnityEngine;

public class AttackAnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttack() => attackState.TriggerAttack();

    private void FinishAttack() => attackState.FinishAttack();
}
