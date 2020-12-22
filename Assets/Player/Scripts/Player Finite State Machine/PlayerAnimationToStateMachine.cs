using UnityEngine;

public class PlayerAnimationToStateMachine : MonoBehaviour
{
    public PlayerState playerState;

    private void AnimationTrigger()
    {
        playerState.AnimationTrigger();
    }

    private void TriggerAttack()
    {
        playerState.TriggerAttack();
    }

    private void FinishAttack()
    {
        playerState.FinishAttack();
    }

    private void AnimationFinishedTrigger()
    {
        playerState.AnimationFinishedTrigger();
    }
}

