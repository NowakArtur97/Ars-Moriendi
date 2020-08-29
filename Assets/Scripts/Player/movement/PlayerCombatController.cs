
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool canAttack = true;
    private bool isAttacking = false;
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float attackDamage = 5f;

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
        controls.Player.AttackPrimary.performed += _ => AttemptToAttack();
    }

    private void Update()
    {
        CheckAttack();
    }

    private void AttemptToAttack()
    {
        if (canAttack)
        {
            isAttacking = true;
        }
    }

    private void CheckAttack()
    {
        if (isAttacking)
        {
            myAnimator.SetBool("isAttacking", isAttacking);
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjectsToAttack = Physics2D.OverlapCircleAll(attackHitBoxPosition.position, attackRadius, whatIsDamagable);

        foreach (Collider2D objectToAttack in detectedObjectsToAttack)
        {
            Debug.Log(objectToAttack.name);
            //objectToAttack.transform.SendMessage("Damage", attackDamage);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;

        myAnimator.SetBool("isAttacking", isAttacking);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPosition.position, attackRadius);
    }
}
