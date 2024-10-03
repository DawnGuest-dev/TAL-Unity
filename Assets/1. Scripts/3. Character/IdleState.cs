using UnityEngine;

public class IdleState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly CharacterController _cc;

    public IdleState(StateMachine stateMachine, CharacterController characterController)
    {
        _stateMachine = stateMachine;
        _cc = characterController;
    }

    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }

    public void Update()
    {
        if (_stateMachine.moveInput.magnitude != 0f)
        {
            _stateMachine.ChangeState(new MoveState(_stateMachine, _cc));
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Exit IdleState");
    }
}
