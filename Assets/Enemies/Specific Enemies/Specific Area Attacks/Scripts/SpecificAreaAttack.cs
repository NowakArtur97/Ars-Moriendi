using UnityEngine;

public class SpecificAreaAttack : MonoBehaviour
{
    private const int PLAYER_LAYER_MASK_VALUE = 1024;

    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private Transform _damagePosition;
    [SerializeField]
    private Vector2 _damageSize;

    private bool _hasAppeared;
    private bool _hasDisappeared;
    private Vector2 _speed;
    private float _startTime;
    private float _timeToDisappear;
    private AttackDetails _attackDetails;
    private RaycastHit2D _damageHit;

    private Rigidbody2D _myRigidbody2D;
    private Animator _myAnimator;

    private void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();

        _myRigidbody2D.velocity = _speed;
        _myAnimator.SetBool("appear", true);

        _startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _startTime + _timeToDisappear)
        {
            _myAnimator.SetBool("appear", false);
            _myAnimator.SetBool("disappear", true);
        }

        if (_hasDisappeared)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_hasAppeared && !_hasDisappeared)
        {
            _damageHit = Physics2D.BoxCast(_damagePosition.position, _damageSize, 0.0f, Vector2.up, _whatIsEnemy);

            if (_damageHit.collider)
            {
                _attackDetails.position = transform.position;

                _damageHit.transform.parent.SendMessage("Damage", _attackDetails);
            }
        }
    }

    public void SpawnAttack(float speed, AttackDetails attackDetails, float timeToDisappear)
    {
        _speed = transform.right * speed;
        _timeToDisappear = timeToDisappear;
        _attackDetails = attackDetails;
    }

    public void AppearedTrigger() => _hasAppeared = true;

    public void DisappearedTrigger() => _hasDisappeared = true;

    private bool IsPlayerEnemy => _whatIsEnemy.value == PLAYER_LAYER_MASK_VALUE;
}
