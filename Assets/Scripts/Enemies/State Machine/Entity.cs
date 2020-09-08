using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform ledgeCheck;

    public D_Entity entityData;

    public FiniteStateMachine finiteStateMachine;

    public Rigidbody2D myRigidbody2D { get; private set; }
    public Animator myAnimator { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    public float facingDirection { get; private set; }

    private Vector2 velocityWorkSpace;

    protected virtual void Start()
    {
        aliveGameObject = transform.Find("Alive").gameObject;
        myRigidbody2D = aliveGameObject.GetComponent<Rigidbody2D>();
        myAnimator = aliveGameObject.GetComponent<Animator>();

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

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public void Flip()
    {
        facingDirection *= -1;

        aliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - entityData.wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x, ledgeCheck.position.y - entityData.ledgeCheckDistance));
    }
}
