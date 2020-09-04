using System;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float movementSpeed;
    private Vector2 movement;

    private State currentState;

    private bool groundDetected, wallDetected;

    private float facingDirection = 1;

    private GameObject aliveGO;
    private Rigidbody2D aliveRigidbody2D;
    private Animator aliveAnimator;
    private SpriteRenderer aliveSpriteRenderer;

    private enum State
    {
        Moving, Knockback, Dead
    }

    private void Awake()
    {
        aliveGO = transform.Find("Boar Alive").gameObject;
        aliveAnimator = aliveGO.GetComponent<Animator>();
        aliveRigidbody2D = aliveGO.GetComponent<Rigidbody2D>();
        aliveSpriteRenderer = aliveGO.GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        currentState = State.Moving;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    #region Moving State
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        CheckPosition();

        if (ShouldFlip())
        {
            Flip();
        }
        //else
        //{
        //    Move();
        //}
    }

    private void ExitMovingState()
    {

    }
    #endregion

    #region Knockback State
    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }
    #endregion

    #region Dead State
    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }
    #endregion

    #region Other
    private void ChangeState(State newState)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (newState)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = newState;
    }

    private void CheckPosition()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - wallCheckDistance, wallCheck.position.y));
    }

    private void Flip()
    {
        aliveSpriteRenderer.transform.Rotate(Vector3.up * -180);
        facingDirection *= -1;
    }

    private bool ShouldFlip()
    {
        return !groundDetected || wallDetected;
    }
    #endregion
}
