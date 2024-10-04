using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private StateObserver stateObserver;

    private InputAction moveAction;
    private InputAction sprintAction;
    
    public bool isSprinting;
    private float _sprintStartTime;
    public float sprintThreshold = 0.5f;
    public float gravity = -9.81f;
    
    public Vector2 moveInput;
    
    private Animator animator;
    private CharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        
    }

    private void OnEnable()
    {
        sprintAction.performed += OnSprintPerformed;
        sprintAction.canceled += OnSprintCanceled;
    }

    private void Start()
    {
        stateObserver = new StateObserver();
        
        ChangeState(new IdleState(this, _cc));
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        _sprintStartTime = Time.time; // 스프린트 시작 시간 기록
        isSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        float sprintDuration = Time.time - _sprintStartTime;

        if (sprintDuration < sprintThreshold)
        {
            if (currentState is IdleState)
            {
                Debug.Log("BackStep");
            }
            else
            {
                Debug.Log("Roll");   
            }
        }

        isSprinting = false;
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
        
        stateObserver.NotifyStateChanged(currentState);
    }

    private Vector3 velocity;
    private float currentBlendValue = 0.0f;
    public float blendSpeed = 5.0f;

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        float targetBlendValue = moveInput.magnitude;
        currentBlendValue = Mathf.Lerp(currentBlendValue, targetBlendValue, Time.deltaTime * blendSpeed);
        animator.SetFloat("Blend", currentBlendValue);

        if (!_cc.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        _cc.Move(velocity * Time.deltaTime);

        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public IState GetCurrentState()
    {
        return currentState;
    }

    public StateObserver GetObserver()
    {
        return stateObserver;
    }
}
