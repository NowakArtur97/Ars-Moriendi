using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    private float fadingTimer;
    [SerializeField] private float defaultAlpha = 0.8f;
    private float curentAlpha;
    [SerializeField] private float alphaMultiplier = 0.85f;

    private Transform playerPosition;
    private SpriteRenderer playerSpriteRenderer;

    private Color spriteColor;
    private SpriteRenderer mySpriteRenderer;

    private void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerPosition = player.transform;
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

        transform.position = playerPosition.position;
        transform.rotation = playerPosition.rotation;

        mySpriteRenderer.sprite = playerSpriteRenderer.sprite;

        curentAlpha = defaultAlpha;
        fadingTimer = Time.time;
    }

    private void Update()
    {
        curentAlpha *= alphaMultiplier * Time.deltaTime;
        spriteColor = new Color(1, 1, 1, curentAlpha);
        mySpriteRenderer.color = spriteColor;

        if ((fadingTimer + activeTime) <= Time.time)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
