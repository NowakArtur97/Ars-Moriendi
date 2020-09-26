using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private D_PlayerData _playerData;

    public PlayerFiniteStateMachine PlayerFiniteStateMachine { get; private set; }

    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }

    public Animator MyAnmator;
    public PlayerInputHandler PlayerInputHandler { get; private set; }

    private void Awake()
    {
        PlayerFiniteStateMachine = new PlayerFiniteStateMachine();

        PlayerIdleState = new PlayerIdleState(this, PlayerFiniteStateMachine, _playerData, "idle");
        PlayerMoveState = new PlayerMoveState(this, PlayerFiniteStateMachine, _playerData, "move");
    }

    private void Start()
    {
        MyAnmator = GetComponent<Animator>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();

        PlayerFiniteStateMachine.Initialize(PlayerIdleState);
    }

    private void Update()
    {
        PlayerFiniteStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        PlayerFiniteStateMachine.CurrentState.PhysicsUpdate();
    }
}
