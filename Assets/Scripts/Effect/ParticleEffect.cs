using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _particle;

    private ParticleSystem _particleEffect;

    private void OnEnable()
    {
        _particleEffect = _particle.GetComponent<ParticleSystem>();

        _particleEffect.Clear();
        _particleEffect.time = 0;
        _particleEffect.Play();
    }

    private void Update()
    {
        // TODO: Object Pooling
        //if (!_particleEffect.isPlaying)
        //{
        //    ObjectPoolManager.Instance.AddToPool(gameObject, ObjectPoolType.MEAT_CHUNK_EFFECT);
        //}
    }
}
