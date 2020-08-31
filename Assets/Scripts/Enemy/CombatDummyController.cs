using System;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private bool isKnockBackEnabled = true;
    [SerializeField] private float maxHealth = 50f;
    private float healthLeft;
    [SerializeField] private float knockBackXForce = 3f;
    [SerializeField] private float knockBackYForce = 0f;

    private float playerFacingDirection;
    private PlayerMovementController playerMovementController;

    private GameObject aliveGO;
    private GameObject deadTopGO;
    private GameObject deadBottomGO;
    private GameObject deadShieldGO;

    private Rigidbody2D aliveRigidBody2D;
    private Rigidbody2D deadTopRigidBody2D;
    private Rigidbody2D deadBottomRigidBody2D;
    private Rigidbody2D deadShieldRigidBody2D;

    private Animator aliveAnimator;

    private void Awake()
    {
        healthLeft = maxHealth;
    }

    private void Start()
    {
        playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();

        aliveGO = transform.Find("Combat Dummy Alive").gameObject;
        deadTopGO = transform.Find("Combat Dummy Dead Top").gameObject;
        deadBottomGO = transform.Find("Combat Dummy Dead Bottom").gameObject;
        deadShieldGO = transform.Find("Combat Dummy Dead Shield").gameObject;

        aliveRigidBody2D = aliveGO.GetComponent<Rigidbody2D>();
        deadTopRigidBody2D = deadTopGO.GetComponent<Rigidbody2D>();
        deadBottomRigidBody2D = deadBottomGO.GetComponent<Rigidbody2D>();
        deadShieldRigidBody2D = deadShieldGO.GetComponent<Rigidbody2D>();

        aliveAnimator = aliveGO.GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void Damage(float damageReceived)
    {
        healthLeft -= damageReceived;

        if (healthLeft <= 0)
        {
            Die();
        }
        else
        if (isKnockBackEnabled)
        {
            SetAnimation();
            KnockBack();
        }
    }

    private void SetAnimation()
    {
        playerFacingDirection = playerMovementController.GetFacingDirection();
        bool isPlayerOnTheLeft = playerFacingDirection == 1;

        aliveAnimator.SetBool("isPlayerOnTheLeft", isPlayerOnTheLeft);
        aliveAnimator.SetTrigger("isDamaged");
    }

    private void KnockBack()
    {
        aliveRigidBody2D.AddForce(new Vector2(knockBackXForce * playerFacingDirection, knockBackYForce), ForceMode2D.Impulse);
    }

    private void Die()
    {
        deadTopGO.transform.position = aliveGO.transform.position;
        deadBottomGO.transform.position = aliveGO.transform.position;
        deadShieldGO.transform.position = aliveGO.transform.position;

        aliveGO.SetActive(false);
        deadTopGO.SetActive(false);
        deadBottomGO.SetActive(false);
        deadShieldGO.SetActive(false);
    }
}
