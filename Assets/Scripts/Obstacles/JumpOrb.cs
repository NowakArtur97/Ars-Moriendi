using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    private float enableTimer = 5f;
    private bool isActive;

    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        isActive = true;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isActive)
        {
            enableTimer -= Time.deltaTime;
            if (enableTimer <= 0)
            {
                ActivateOrb();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TouchedJumpOrb();

            DectivateOrb();
        }
    }

    private void ActivateOrb()
    {
        isActive = true;
        mySpriteRenderer.color = new Color(0, 49, 255, 1f);
    }

    private void DectivateOrb()
    {
        isActive = false;
        enableTimer = 5f;
        mySpriteRenderer.color = new Color(0, 49, 255, 0.5f);
    }
}
