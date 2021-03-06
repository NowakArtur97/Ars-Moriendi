﻿using System;
using UnityEngine;
using static PlayerCombatController;

public class BasicEnemyController : MonoBehaviour
{
    [Header("Position Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private State currentState = State.Moving;

    [Header("Movement")]
    [Range(0, 10)]
    [SerializeField] private float movementSpeed;
    private Vector2 movement;

    [Header("Health/Death")]
    [Range(0, 300)]
    [SerializeField] private float maxHealth = 50f;
    private float healthLeft;

    [Header("Knockback")]
    [Range(0, 10)]
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;
    private float knockbackStartTime;
    private int damageDirection;

    [Header("Touch Damage")]
    [Range(0, 10)]
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamageWidth;
    [SerializeField] private float touchDamageHeight;
    [SerializeField] private Transform touchDamageCheck;
    private float lastTouchDamageTime;
    private Vector2 touchDamageBottomLeft;
    private Vector2 touchDamageUpperRight;

    private bool groundDetected, wallDetected;

    private float facingDirection = 1;

    private GameObject aliveGO;
    private Rigidbody2D aliveRigidbody2D;
    private Animator aliveAnimator;
    private SpriteRenderer aliveSpriteRenderer;

    private PlayerMovementController playerMovement;

    private AttackDetails attackDetails;

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

        healthLeft = maxHealth;
    }

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovementController>();
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
        CheckTouchDamage();

        if (ShouldFlip())
        {
            Flip();
        }
        else
        {
            Move();
        }
    }

    private void ExitMovingState()
    {

    }
    #endregion

    #region Knockback State
    private void EnterKnockbackState()
    {
        aliveAnimator.SetBool("isDamaged", true);
        ObjectPoolManager.Instance.GetFromPool(ObjectPoolType.BLOOD_PARTICLE_EFFECT);

        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRigidbody2D.velocity = movement;
    }

    private void UpdateKnockbackState()
    {
        if (Time.time > knockbackStartTime + knockbackDuration)
        {
            ChangeState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnimator.SetBool("isDamaged", false);
    }
    #endregion

    #region Dead State
    private void EnterDeadState()
    {
        aliveAnimator.SetBool("isDead", true);
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

    private void Move()
    {
        movement.Set(facingDirection * movementSpeed, aliveRigidbody2D.velocity.y);
        aliveRigidbody2D.velocity = movement;
    }

    private void Damage(AttackDetails attackDetails)
    {
        healthLeft -= attackDetails.damageAmmount;
        damageDirection = attackDetails.position.x > aliveGO.transform.position.x ? -1 : 1;

        if (healthLeft <= 0.1f)
        {
            ChangeState(State.Dead);
        }
        else
        {
            ChangeState(State.Knockback);
        }
    }

    private void CheckTouchDamage()
    {
        if (!playerMovement.GetDashStatus() && Time.time > lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBottomLeft.Set(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
            touchDamageUpperRight.Set(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);

            Collider2D hit = Physics2D.OverlapArea(touchDamageBottomLeft, touchDamageUpperRight, whatIsPlayer);

            attackDetails.position = transform.position;
            attackDetails.damageAmmount = 0;

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;

                hit.SendMessage("Damage", attackDetails);
            }
        }
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

        Vector2 bottomLeftTouchDamage = new Vector2(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
        Vector2 bottomRightTouchDamage = new Vector2(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y - touchDamageHeight / 2);
        Vector2 topLeftTouchDamage = new Vector2(touchDamageCheck.position.x - touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);
        Vector2 topRightTouchDamage = new Vector2(touchDamageCheck.position.x + touchDamageWidth / 2, touchDamageCheck.position.y + touchDamageHeight / 2);

        Gizmos.DrawLine(bottomLeftTouchDamage, bottomRightTouchDamage);
        Gizmos.DrawLine(bottomRightTouchDamage, topRightTouchDamage);
        Gizmos.DrawLine(topRightTouchDamage, topLeftTouchDamage);
        Gizmos.DrawLine(topLeftTouchDamage, bottomLeftTouchDamage);
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
