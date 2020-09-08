using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine finiteStateMachine;

    public Rigidbody2D myRigidbody2D { get; private set; }
    public Animator myAnimator { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    public float facingDirection { get; private set; }

    private Vector2 velocityWorkSpace;

    protected virtual void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        aliveGameObject = transform.Find("Alive").gameObject;

        finiteStateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        finiteStateMachine.currentState.LogicUpdateFunction();
    }

    protected virtual void FixedUpdate()
    {
        finiteStateMachine.currentState.PhysicsUpdateFunction();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(velocity * facingDirection, myRigidbody2D.velocity.y);

        myRigidbody2D.velocity = velocityWorkSpace;
    }
}
