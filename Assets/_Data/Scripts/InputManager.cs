using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput _playerInput;

    [Header("Ingame Inputs")]
    private Vector2 _moveInput;
    private Vector2 _cameraInput;

    public PlayerInput PlayerInput => _playerInput;
    public Vector2 MoveInput => _moveInput;

    public Vector2 CameraInput => _cameraInput;

    public float ScrollInput { get; private set; }
    public bool IsMovePressing { get; private set; }
    public bool IsShiftPressing { get; private set; }
    public bool IsSpacePressing { get; private set; }
    public bool IsLeftClicking { get; private set; }
    public bool IsRightClicking { get; private set; }

    [Header("UI Inputs")]
    public bool IsPausePressed { get; private set; }

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

    public void OnMovePressingEvent(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsMovePressing = _moveInput.x != 0 || _moveInput.y != 0;
        if (context.canceled)
        {
            _moveInput = Vector2.zero;
            IsMovePressing = false;
            IsShiftPressing = false;
        }
    }

    public void OnShiftPressingEvent(InputAction.CallbackContext context)
    {
        IsShiftPressing = context.ReadValueAsButton();
    }

    public void OnSpacePressingEvent(InputAction.CallbackContext context)
    {
        IsSpacePressing = context.ReadValueAsButton();
    }

    public void OnLeftClickingEvent(InputAction.CallbackContext context)
    {
        IsLeftClicking = context.ReadValueAsButton();
    }

    public void OnRightClickingEvent(InputAction.CallbackContext context)
    {
        IsRightClicking = context.ReadValueAsButton();
    }

    public void OnScrollEvent(InputAction.CallbackContext context)
    {
        ScrollInput = context.ReadValue<Vector2>().y; 
    }

    public void OnEscapePressingEvent(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsPausePressed = !IsPausePressed;
            _playerInput.SwitchCurrentActionMap(IsPausePressed ? Const.UI : Const.Player);
        }
    }


}