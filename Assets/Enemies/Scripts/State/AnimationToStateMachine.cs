using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public EnemyState state;

    private void AnimationTrigger() => state.AnimationTrigger();

    private void AnimationFinishedTrigger() => state.AnimationFinishedTrigger();
}
