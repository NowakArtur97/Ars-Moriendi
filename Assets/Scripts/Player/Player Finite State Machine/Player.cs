using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private D_PlayerData _playerData;

    public PlayerFiniteStateMachine playerFiniteStateMachine { get; private set; }

    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }

    public Animator MyAnmator;

    private void Awake()
    {
        playerFiniteStateMachine = new PlayerFiniteStateMachine();
    }

    private void Start()
    {
        MyAnmator = GetComponent<Animator>();

        PlayerIdleState = new PlayerIdleState(this, playerFiniteStateMachine, _playerData, "idle");
        PlayerMoveState = new PlayerMoveState(this, playerFiniteStateMachine, _playerData, "move");

        playerFiniteStateMachine.Initialize(PlayerIdleState);
    }

    private void Update()
    {
        playerFiniteStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        playerFiniteStateMachine.CurrentState.PhysicsUpdate();
    }
}
