using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어 캐릭터의 Transform
    public float mouseSensitivity = 100f; // 마우스 감도
    public float clampAngle = 85f; // 카메라 회전 각도 제한

    private float rotationY = 0f; // Y축 회전
    private float rotationX = 0f; // X축 회전
    
    private InputAction lookAction;
    public Vector2 lookInput;

    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 잠급니다.
    }

    void Update()
    {
        lookInput = lookAction.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // X축 회전 (상하)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        // Y축 회전 (좌우)
        rotationY += mouseX;

        // 카메라 회전 적용
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}