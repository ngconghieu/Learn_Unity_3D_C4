using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private Vector2 _cameraInput;

    public PlayerInput PlayerInput => _playerInput;
    public Vector2 MoveInput => _moveInput;

    public Vector2 CameraInput => _cameraInput;
    public float ScrollInput { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsLeftClicking { get; private set; }
    public bool IsRightClicking { get; private set; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerInput();
    }

    private void LoadPlayerInput()
    {
        if (_playerInput != null) return;
        _playerInput = GetComponent<PlayerInput>();
        Debug.LogWarning("LoadPlayerInput", gameObject);
    }

    public void OnLookEvent(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsWalking = _moveInput.x != 0 || _moveInput.y != 0;
        if (context.canceled)
        {
            _moveInput = Vector2.zero;
            IsWalking = false;
            IsRunning = false;
        }
    }

    public void OnSprintEvent(InputAction.CallbackContext context)
    {
        IsRunning = context.ReadValueAsButton();
    }

    public void OnJumpEvent(InputAction.CallbackContext context)
    {
        IsJumping = context.ReadValueAsButton();
    }

    public void OnAttackEvent(InputAction.CallbackContext context)
    {
        IsLeftClicking = context.ReadValueAsButton();
    }

    public void OnBlockEvent(InputAction.CallbackContext context)
    {
        IsRightClicking = context.ReadValueAsButton();
    }

    public void OnScrollEvent(InputAction.CallbackContext context)
    {
        ScrollInput = context.ReadValue<Vector2>().y; 
    }
}