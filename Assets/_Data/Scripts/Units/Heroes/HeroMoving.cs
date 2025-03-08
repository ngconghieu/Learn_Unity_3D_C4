using System;
using UnityEngine;

public class HeroMoving : HeroAbstract
{
    [SerializeField] protected float walkingSpeed = 5f;
    [SerializeField] protected float runningSpeedMultiplier = 1.8f;
    [SerializeField] protected float rotateSpeed = 8;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isRunning = false;
    protected Vector3 _movingDirection;
    private Vector2 _movingInput;
    private Quaternion _yAxis;

    private void Update()
    {
        CheckMovement();
        AnimationHandling();
        CheckMoving();
    }

    private void CheckMovement()
    {
        isWalking = InputManager.Instance.IsWalking;
        isRunning = InputManager.Instance.IsRunning;
    }

    private void AnimationHandling()
    {
        HeroCtrl.Animator.SetBool(Const.IsRunning, isRunning);
        HeroCtrl.Animator.SetBool(Const.IsWalking, isWalking);
    }

    private void CheckMoving()
    {
        if (!isWalking) return;
        _movingInput = InputManager.Instance.MoveInput;
        //Get rotation of hero though camera
        _yAxis = Quaternion.Euler(0, HeroCtrl.CameraCtrl.ControlRotation.y, 0);
        Vector3 forward = _yAxis * Vector3.forward;
        Vector3 right = _yAxis * Vector3.right;
        _movingDirection = (forward * _movingInput.y + right * _movingInput.x).normalized;
        float speed = isRunning ? (runningSpeedMultiplier * walkingSpeed) : walkingSpeed;
        RotateHero();
        Moving(speed);
    }

    private void RotateHero()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_movingDirection, Vector3.up);
        transform.parent.rotation = Quaternion.Slerp(
            transform.parent.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed);
    }

    private void Moving(float speed)
    {
        HeroCtrl.CharacterController.Move(speed * Time.deltaTime * _movingDirection);
    }
}
