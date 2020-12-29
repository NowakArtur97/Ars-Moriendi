using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _gravity = 4.0f;
    [SerializeField]
    private bool _isGravityOnStart = false;

    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private Transform _damagePosition;
    [SerializeField]
    private float _damageRadius = 0.15f;

    private AttackDetails _attackDetails;
    private Vector2 _speed;
    private float _gravityScale;
    private float _angle = 0;
    private float _travelDistance;
    private float _xStartPosition;

    private bool _isGravityOn;
    private bool _hasHitGround;

    private Rigidbody2D _myRigidbody2D;
    private Animator _myAnimator;

    private void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();

        _myRigidbody2D.gravityScale = _gravityScale;
        _myRigidbody2D.velocity = _speed;

        _xStartPosition = transform.position.x;

        _isGravityOn = _isGravityOnStart;
        _hasHitGround = false;
    }

    private void Update()
    {
        if (_isGravityOn)
        {
            float angle = Mathf.Atan2(_myRigidbody2D.velocity.y, _myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
                _attackDetails.position = transform.position;
                // TODO: Add alive child object in Player
                damageHit.transform.parent.SendMessage("Damage", _attackDetails);

                Destroy(gameObject);
            }

            if (groundHit)
            {
                _hasHitGround = true;
            }

            if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance)
            {
                _isGravityOn = true;
                _myRigidbody2D.gravityScale = _gravity;
            }
        }

        if (_hasHitGround)
        {
            if (_myAnimator != null)
            {
                _myAnimator.SetBool("disabled", true);
            }
            _myRigidbody2D.velocity = Vector2.zero;
            _myRigidbody2D.gravityScale = 0.0f;
        }
    }

    public void FireProjectile(float speed, float travelDistance, AttackDetails attackDetails, float gravityScale)
    {
        _speed = transform.right * speed;
        _travelDistance = travelDistance;
        _gravityScale = gravityScale;
        _attackDetails = attackDetails;
    }

    public void FireProjectile(float speed, float travelDistance, AttackDetails attackDetails, float gravityScale, Vector2 direction)
    {
        _speed = direction * speed;
        _travelDistance = travelDistance;
        _gravityScale = gravityScale;
        _attackDetails = attackDetails;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
