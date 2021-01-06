using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public State state;

    private void AnimationTrigger() => state.AnimationTrigger();

    private void AnimationFinishedTrigger() => state.AnimationFinishedTrigger();
}
