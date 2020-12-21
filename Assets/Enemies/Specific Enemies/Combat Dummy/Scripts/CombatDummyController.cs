using System;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private bool _isKnockBackEnabled = true;
    [SerializeField] private float _maxHealth = 50f;
    private float _healthLeft;
    [SerializeField] private float _knockBackXForce = 3f;
    [SerializeField] private float _knockBackYForce = 0f;

    [SerializeField] private float _deadTopXForce = 2f;
    [SerializeField] private float _deadTopYForce = 2f;
    [SerializeField] private float _deadBottomXForce = 1f;
    [SerializeField] private float _deadBottomYForce = 1f;
    [SerializeField] private float _deadShieldXForce = 2.5f;
    [SerializeField] private float _deadShieldYForce = 2.5f;

    [SerializeField] private GameObject _damagePartcileEffect;

    private float _playerFacingDirection;
    private PlayerMovementController _playerMovementController;

    private GameObject _aliveGO;
    private GameObject _deadTopGO;
    private GameObject _deadBottomGO;
    private GameObject _deadShieldGO;

    private Rigidbody2D _aliveRigidBody2D;
    private Rigidbody2D _deadTopRigidBody2D;
    private Rigidbody2D _deadBottomRigidBody2D;
    private Rigidbody2D _deadShieldRigidBody2D;

    private Animator _aliveAnimator;

    private void Awake()
    {
        _healthLeft = _maxHealth;
    }

    private void Start()
    {
        _playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();

        _aliveGO = transform.Find("Combat Dummy Alive").gameObject;
        _deadTopGO = transform.Find("Combat Dummy Dead Top").gameObject;
        _deadBottomGO = transform.Find("Combat Dummy Dead Bottom").gameObject;
        _deadShieldGO = transform.Find("Combat Dummy Dead Shield").gameObject;

        _aliveRigidBody2D = _aliveGO.GetComponent<Rigidbody2D>();
        _deadTopRigidBody2D = _deadTopGO.GetComponent<Rigidbody2D>();
        _deadBottomRigidBody2D = _deadBottomGO.GetComponent<Rigidbody2D>();
        _deadShieldRigidBody2D = _deadShieldGO.GetComponent<Rigidbody2D>();

        _aliveAnimator = _aliveGO.GetComponent<Animator>();
    }

    private void Damage(AttackDetails attackDetails)
    {
        _healthLeft -= attackDetails.damageAmmount;

        if (_healthLeft <= 0)
        {
            Die();
        }
        else if (_isKnockBackEnabled)
        {
            SetAnimation(attackDetails.position.x);
            KnockBack();
            InstantiateParticleEffect();
        }
    }

    private void InstantiateParticleEffect()
    {
        Instantiate(_damagePartcileEffect, _aliveGO.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f)));
    }

    private void SetAnimation(float xPosition)
    {
        _playerFacingDirection = xPosition > _aliveGO.transform.position.x ? -1 : 1;
        bool isPlayerOnTheLeft = _playerFacingDirection == 1;

        _aliveAnimator.SetBool("isPlayerOnTheLeft", isPlayerOnTheLeft);
        _aliveAnimator.SetTrigger("isDamaged");
    }

    private void KnockBack() => _aliveRigidBody2D.AddForce(new Vector2(_knockBackXForce * _playerFacingDirection, _knockBackYForce), ForceMode2D.Impulse);

    private void Die()
    {
        _aliveGO.SetActive(false);
        _deadTopGO.SetActive(true);
        _deadBottomGO.SetActive(true);
        _deadShieldGO.SetActive(true);

        _deadTopGO.transform.position = _aliveGO.transform.position;
        _deadBottomGO.transform.position = _aliveGO.transform.position;
        _deadShieldGO.transform.position = _aliveGO.transform.position;

        _deadTopRigidBody2D.AddForce(new Vector2(_deadTopXForce * _playerFacingDirection, _deadTopYForce), ForceMode2D.Impulse);
        _deadBottomRigidBody2D.AddForce(new Vector2(_deadBottomXForce * _playerFacingDirection, _deadBottomYForce), ForceMode2D.Impulse);
        _deadShieldRigidBody2D.AddForce(new Vector2(_deadShieldXForce * _playerFacingDirection, _deadShieldYForce), ForceMode2D.Impulse);
    }
}
