using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputSystem_Actions _inputActions;
    private Vector2 _movement;
    public Vector2 Movement => _movement;

    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new();

    }

    private void RegisterEvents()
    {
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Sprint.performed += OnSprintPerformed;
        _inputActions.Player.Sprint.canceled += OnSprintCanceled;
    }

    private void UnRegisterEvent()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Sprint.performed -= OnSprintPerformed;
        _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        IsRunning = false;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movement = Vector2.zero;
        IsWalking = false;
        IsRunning = false;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>();
        IsWalking = _movement.x != 0 || _movement.y != 0;
    }

    private void OnSprintPerformed(InputAction.CallbackContext ctx)
    {
        IsRunning = IsWalking && ctx.ReadValueAsButton();
    }

    private void OnEnable()
    {
        RegisterEvents();
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        UnRegisterEvent();
        _inputActions.Player.Disable();
    }
}
