using System;
using UnityEngine;

public class HeroMoving : HeroAbstract
{
    [SerializeField] protected Vector3 movingDirection;
    [SerializeField] protected float walkingSpeed = 5f;
    [SerializeField] protected float runningSpeedX = 1.5f;
    [SerializeField] protected float rotateSpeed = 10f;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isRunning = false;
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
        movingDirection = (forward * _movingInput.y + right * _movingInput.x).normalized;
        float speed = isRunning ? (runningSpeedX * walkingSpeed) : walkingSpeed;
        RotateHero();
        Moving(speed);
    }

    private void RotateHero()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movingDirection, Vector3.up);
        transform.parent.rotation = Quaternion.Slerp(
            transform.parent.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed);
    }

    private void Moving(float speed)
    {
        HeroCtrl.CharacterController.Move(speed * Time.deltaTime * movingDirection);
    }
}
