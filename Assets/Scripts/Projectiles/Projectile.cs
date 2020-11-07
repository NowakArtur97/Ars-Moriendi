using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _gravity = 4.0f;

    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private Transform _damagePosition;
    [SerializeField]
    private float _damageRadius = 0.15f;

    private AttackDetails attackDetails;

    private Vector2 _speed;
    private float _angle = 0;
    private float _travelDistance;
    private float _xStartPosition;

    private bool _isGravityOn;
    private bool _hasHitGround;

    private Rigidbody2D _myRigidbody2D;

    private void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myRigidbody2D.gravityScale = 0.0f;

        _myRigidbody2D.velocity = _speed;

        _xStartPosition = transform.position.x;

        _isGravityOn = false;
        _hasHitGround = false;
    }

    private void Update()
    {
        if (!_hasHitGround)
        {
            attackDetails.position = transform.position;

            if (_isGravityOn)
            {
                float angle = Mathf.Atan2(_myRigidbody2D.velocity.y, _myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsEnemy);
            Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsGround);

            if (damageHit)
            {
                damageHit.transform.parent.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                _hasHitGround = true;
                _myRigidbody2D.gravityScale = 0.0f;
                _myRigidbody2D.velocity = Vector2.zero;
            }

            if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance && !_isGravityOn)
            {
                _isGravityOn = true;
                _myRigidbody2D.gravityScale = _gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        _speed = speed * transform.right;
        _travelDistance = travelDistance;
        attackDetails.damageAmmount = damage;
    }

    public void FireProjectile(float speed, float travelDistance, float damage, Vector2 direction)
    {
        _speed = speed * direction;
        _travelDistance = travelDistance;
        attackDetails.damageAmmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
