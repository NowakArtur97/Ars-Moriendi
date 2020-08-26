using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    private float fadingTimer;
    [SerializeField] private float defaultAlpha = 0.8f;
    private float curentAlpha;
    [SerializeField] private float alphaMultiplier = 0.85f;

    private Transform playerPosition;
    [SerializeField] private Sprite playerSprite;

    private Color spriteColor;
    private SpriteRenderer mySpriteRenderer;

    private void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = playerPosition.position;
        transform.rotation = playerPosition.rotation;

        mySpriteRenderer.sprite = playerSprite;

        curentAlpha = defaultAlpha;
        fadingTimer = Time.time;
    }

    private void Update()
    {
        curentAlpha *= alphaMultiplier;
        spriteColor = new Color(1f, 1f, 1f, curentAlpha);
        mySpriteRenderer.color = spriteColor;

        if ((fadingTimer + activeTime) <= Time.time)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
