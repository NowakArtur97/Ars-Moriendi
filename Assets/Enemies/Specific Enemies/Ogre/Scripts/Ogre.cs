using UnityEngine;

public class Ogre : Enemy
{
    [Header("States Data")]
    [SerializeField] private D_IdleState _idleStateData;
    [SerializeField] private D_MoveState _moveStateData;
    [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] private D_StompAttackState _stompAttackStateData;
    [SerializeField] private D_DamageState _damageStateData;
    [SerializeField] private D_StunState _stunStateData;
    [SerializeField] private D_DeadState _deadStateData;

    [Header("Effects Data")]
    [SerializeField] public D_DissolveEffect DissolveEffectData;

    [Header("Attack Positions")]
    [SerializeField] private Transform _meleeAttackPosition;
    [SerializeField] private Transform _stompAttackPosition;

    public Ogre_IdleState IdleState { get; private set; }
    public Ogre_MoveState MoveState { get; private set; }
    public Ogre_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Ogre_LookForPlayerState LookForPlayerState { get; private set; }
    public Ogre_MeleeAttackState MeleeAttackState { get; private set; }
    public Ogre_StompAttackState StompAttackState { get; private set; }
    public Ogre_DamageState DamageState { get; private set; }
    public Ogre_StunState StunState { get; private set; }
    public Ogre_DeadState DeadState { get; private set; }

    public DissolveEffect DissolveEffect { get; private set; }

    protected override void Start()
    {
        base.Start();

        IdleState = new Ogre_IdleState(FiniteStateMachine, this, "idle", _idleStateData, this);
        MoveState = new Ogre_MoveState(FiniteStateMachine, this, "jumpingMove", _moveStateData, this);
        PlayerDetectedState = new Ogre_PlayerDetectedState(FiniteStateMachine, this, "playerDetected", _playerDetectedStateData, this);
        // TODO: OGRE create look for player animation
        LookForPlayerState = new Ogre_LookForPlayerState(FiniteStateMachine, this, "lookForPlayer", _lookForPlayerStateData, this);
        MeleeAttackState = new Ogre_MeleeAttackState(FiniteStateMachine, this, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        StompAttackState = new Ogre_StompAttackState(FiniteStateMachine, this, "stompAttack", _stompAttackPosition, _stompAttackStateData, this);
        DamageState = new Ogre_DamageState(FiniteStateMachine, this, "damage", _damageStateData, this);
        StunState = new Ogre_StunState(FiniteStateMachine, this, "stun", _stunStateData, this);
        DeadState = new Ogre_DeadState(FiniteStateMachine, this, "dead", _deadStateData, this);

        FiniteStateMachine.Initialize(IdleState);

        DissolveEffect = new DissolveEffect();
    }

    public override void Damage(AttackDetails attackDetails)
    {
        if (DeadState.IsDead)
        {
            return;
        }

        bool canDamage = Time.time >= StatsManager.LastDamageTime + _damageStateData.timeBeforeNextDamage;

        base.Damage(attackDetails);

        foreach (GameObject hitPartcile in _damageStateData.hitPartciles)
        {
            GameObject.Instantiate(hitPartcile, AliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        }

        if (StatsManager.IsDead)
        {
            FiniteStateMachine.ChangeState(DeadState);
        }
        else if (canDamage)
        {
            FiniteStateMachine.ChangeState(DamageState);
        }
    }
}
