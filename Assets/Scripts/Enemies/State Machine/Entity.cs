using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D { get; private set; }
    public Animator myAnimator { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    public virtual void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        aliveGameObject = transform.Find("Alive").gameObject;
    }
}
