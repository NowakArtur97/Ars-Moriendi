using UnityEngine;

public class SlashParticleEffect : MonoBehaviour
{
    private void DestroyOnAnimationFinish()
    {
        Destroy(gameObject);
    }
}
