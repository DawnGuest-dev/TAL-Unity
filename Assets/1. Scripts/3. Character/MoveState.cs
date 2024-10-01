using UnityEngine;

public class MoveState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly Rigidbody _rb;
    
    public MoveState(StateMachine stateMachine, Rigidbody rb)
    {
        _stateMachine = stateMachine;
        _rb = rb;
    }
    
    public void Enter()
    {
        Debug.Log("Enter MoveState");
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        // 이동 로직
        Vector3 movement = new Vector3(_stateMachine.moveInput.x, 0, _stateMachine.moveInput.y);
        _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);

        // 점프와 대기 상태로 전환
        if (_stateMachine.moveInput == Vector2.zero)
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine, _rb));
        }
    }

    public void SetMoveInput(Vector2 input)
    {
        
    }

    public void Exit()
    {
        Debug.Log("Exit MoveState");
    }
}
