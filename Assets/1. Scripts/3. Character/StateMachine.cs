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
    
    public Vector2 moveInput;
    
    private Animator animator;

    private void Awake()
    {
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
        
        animator = GetComponentInChildren<Animator>();
        
        ChangeState(new IdleState(this, GetComponent<CharacterController>()));
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

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        animator.SetFloat("Blend", moveInput.magnitude);

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
