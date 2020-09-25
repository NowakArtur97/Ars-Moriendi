using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerFiniteStateMachine playerFiniteStateMachine { get; private set; }

    private void Awake()
    {
        playerFiniteStateMachine = new PlayerFiniteStateMachine();
    }

    private void Start()
    {
        //TODO: setup playerFiniteStateMachine
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
