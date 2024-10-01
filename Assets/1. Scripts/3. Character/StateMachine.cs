using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private StateObserver stateObserver;

    private InputAction moveAction;
    public Vector2 moveInput;
    
    private Animator animator;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        stateObserver = new StateObserver();
        
        animator = GetComponentInChildren<Animator>();
        
        ChangeState(new IdleState(this, GetComponent<Rigidbody>()));
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
