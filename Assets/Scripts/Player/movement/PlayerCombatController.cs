
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled = true;
    private bool isAttacking, isFirstAttack, attack1;
    [SerializeField] private float attackRadius = 0.8f;
    [SerializeField] private float attackDamage = 5f;

    [SerializeField] private float prematureAttackAttemptDefaultTimer = 0.15f;
    private float prematureAttackAttemptTimer = 0;
    private bool isAttemptingToAttack;

    [SerializeField] private Transform attackHitBoxPosition;
    [SerializeField] private LayerMask whatIsDamagable;

    private InputMaster controls;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();

        controls = new InputMaster();
    }

    private void Start()
    {
        myAnimator.SetBool("canAttack", combatEnabled);

        controls.Player.AttackPrimary.performed += _ => AttemptToAttack();
    }

    private void Update()
    {
        CheckAttack();
    }

    private void AttemptToAttack()
    {
        if (combatEnabled)
        {
            if (isAttacking)
            {
                isAttemptingToAttack = true;
                prematureAttackAttemptTimer = prematureAttackAttemptDefaultTimer;
            }
            else
            {
                isAttacking = true;
            }
        }
    }

    private void CheckAttack()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isFirstAttack = !isFirstAttack;
        attack1 = true;

        myAnimator.SetBool("isAttacking", isAttacking);
        myAnimator.SetBool("isFirstAttack", isFirstAttack);
        myAnimator.SetBool("attack1", attack1);

        isAttacking = false;
    }

    private void FinishAttack()
    {
        attack1 = false;

        myAnimator.SetBool("isAttacking", isAttacking);
        myAnimator.SetBool("attack1", attack1);
    }

    private void CheckAttackHitBoxEvent()
    {
        Collider2D[] detectedObjectsToAttack = Physics2D.OverlapCircleAll(attackHitBoxPosition.position, attackRadius, whatIsDamagable);

        foreach (Collider2D objectToAttack in detectedObjectsToAttack)
        {
            Debug.Log(objectToAttack.name);
            //objectToAttack.transform.SendMessage("Damage", attackDamage);
        }
    }

    private void FinishAttackEvent()
    {
        FinishAttack();

        if (isAttemptingToAttack && prematureAttackAttemptTimer > 0)
        {
            isAttemptingToAttack = false;
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPosition.position, attackRadius);
    }
}
