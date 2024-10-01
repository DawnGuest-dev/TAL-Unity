using UnityEngine;

public class IdleState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly Rigidbody _rb;

    public IdleState(StateMachine stateMachine, Rigidbody rb)
    {
        _stateMachine = stateMachine;
        _rb = rb;
    }

    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }

    public void Update()
    {
        if (_stateMachine.moveInput.magnitude != 0f)
        {
            _stateMachine.ChangeState(new MoveState(_stateMachine, _rb));
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
