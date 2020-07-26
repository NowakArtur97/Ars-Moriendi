using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TouchedJumpOrb();
        }
    }
}
