using UnityEngine;

public class BloodParticleEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 positionOffset = new Vector2(1.5f, -0.5f);

    private ParticleSystem bloodPrticleEffect;

    private Transform playerPosition;

    private float facingDirection;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        facingDirection = player.GetComponent<PlayerMovementController>().GetFacingDirection();

        bloodPrticleEffect = gameObject.GetComponent<ParticleSystem>();

        Vector2 position = new Vector2(playerPosition.position.x + positionOffset.x * facingDirection,
            playerPosition.position.y + positionOffset.y);
        transform.position = position;

        bloodPrticleEffect.Clear();
        bloodPrticleEffect.time = 0;
        bloodPrticleEffect.Play();
    }

    private void Update()
    {
        if (!bloodPrticleEffect.isPlaying)
        {
            ObjectPoolManager.Instance.AddToPool(gameObject, ObjectPoolType.AFTER_IMAGE);
        }
    }
}
