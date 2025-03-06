using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputSystem_Actions _inputActions;
    private Vector2 _movement;
    private Vector2 _mouseRotation;
    public Vector2 Movement => _movement;

    public Vector2 MouseRotation => _mouseRotation;
    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsLeftClicking { get; private set; }
    public bool IsRightClicking { get; private set; }

    #region initialization
    protected override void Awake()
    {
        base.Awake();
        _inputActions = new();
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
    #endregion

    private void RegisterEvents()
    {
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Sprint.performed += OnSprintPerformed;
        _inputActions.Player.Sprint.canceled += OnSprintCanceled;
        _inputActions.Player.Look.performed += OnLookPerformed;
        _inputActions.Player.Look.canceled += OnLookCanceled;
        _inputActions.Player.Attack.performed += OnAttackPerformed;

    }

    private void UnRegisterEvent()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Sprint.performed -= OnSprintPerformed;
        _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
        _inputActions.Player.Look.performed -= OnLookPerformed;
        _inputActions.Player.Look.canceled -= OnLookCanceled;
        _inputActions.Player.Attack.performed -= OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        IsLeftClicking = context.ReadValueAsButton();
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _mouseRotation = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        _mouseRotation = Vector2.zero;
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

}
