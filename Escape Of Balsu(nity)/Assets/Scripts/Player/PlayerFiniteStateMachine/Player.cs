using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField]
    private PlayerData playerData;
    private void Awake() 
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }
    private void Start() 
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState); // Initialize statemachine with idle state
    }
    private void Update() 
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate() 
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
