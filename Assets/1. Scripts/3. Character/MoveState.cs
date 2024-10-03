using UnityEngine;

public class MoveState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly CharacterController _cc;
    
    private float _moveSpeed = 3f; // 이동 속도 변수 추가
    private float _runSpeed = 5f; // 이동 속도 변수 추가
    
    public MoveState(StateMachine stateMachine, CharacterController characterController)
    {
        _stateMachine = stateMachine;
        _cc = characterController;
    }
    
    public void Enter()
    {
        Debug.Log("Enter MoveState");
    }

    public void Update()
    {
        Vector3 moveDirection = GetCameraRelativeMovement();
        
        _cc.Move(moveDirection * Time.deltaTime * (_stateMachine.isSprinting ? _runSpeed : _moveSpeed));
        
        // 캐릭터 회전 처리
        if (moveDirection != Vector3.zero)
        {
            _stateMachine.transform.rotation = Quaternion.Slerp(_stateMachine.transform.rotation, Quaternion.LookRotation(moveDirection), 0.1f);
        }
        
        // 점프와 대기 상태로 전환
        if (_stateMachine.moveInput == Vector2.zero)
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine, _cc));
        }
    }

    public void FixedUpdate()
    {
        
    }

    private Vector3 GetCameraRelativeMovement()
    {
        // 카메라 방향 벡터를 기준으로 이동 입력을 변환
        Transform cameraTransform = Camera.main.transform;

        // 카메라의 전/후 방향 (y 축 방향은 고려하지 않음)
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        // 카메라의 좌/우 방향
        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        // 입력받은 이동 값을 카메라 방향으로 변환
        Vector3 movement = forward * _stateMachine.moveInput.y + right * _stateMachine.moveInput.x;
        return movement;
    }
    
    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed; // 외부에서 속도 조절 가능하게 메서드 추가
    }

    public void Exit()
    {
        Debug.Log("Exit MoveState");
    }
}
