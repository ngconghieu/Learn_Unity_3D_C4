using System;
using UnityEngine;

public class HeroMoving : GameMonoBehaviour
{
    [SerializeField] protected HeroCtrl heroCtrl;
    [SerializeField] protected Vector3 movingDirection = Vector3.zero;
    [SerializeField] protected float walkingSpeed = 5f;
    [SerializeField] protected float runningSpeedX = 1.5f;
    [SerializeField] protected float rotateSpeed = 10f;
    [SerializeField] protected float gravity = -0.8f;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isRunning = false;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHeroCtrl();
    }

    private void LoadHeroCtrl()
    {
        if (heroCtrl != null) return;
        heroCtrl = GetComponentInParent<HeroCtrl>();
        Debug.LogWarning("LoadHeroCtrl", gameObject);
    }
    #endregion

    private void Update()
    {
        CheckMovement();
        AnimationHandling();

    }

    private void FixedUpdate()
    {
        CheckMoving();
    }

    private void CheckMovement()
    {
        isWalking = InputManager.Instance.IsWalking;
        isRunning = InputManager.Instance.IsRunning;
    }

    private void AnimationHandling()
    {
        heroCtrl.Animator.SetBool(Const.IsRunning, isRunning);
        heroCtrl.Animator.SetBool(Const.IsWalking, isWalking);
    }

    private void CheckMoving()
    {
        if (!isWalking)
        {
            heroCtrl.Rigidbody.linearVelocity = new Vector3(0, heroCtrl.Rigidbody.linearVelocity.y, 0);
            return;
        }
        Vector2 movement = InputManager.Instance.MoveInput;
        movingDirection = new Vector3(movement.x, movingDirection.y, movement.y).normalized;
        RotateHero();
        float speed = isRunning ? (runningSpeedX * walkingSpeed) : walkingSpeed;
        Moving(speed);
    }

    private void RotateHero()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movingDirection);
        transform.parent.rotation = Quaternion.Slerp(
            transform.parent.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed);
    }

    private void Moving(float speed)
    {

        //transform.parent.Translate(Time.deltaTime * speed * movingDirection, Space.World);
        heroCtrl.Rigidbody.linearVelocity = new Vector3(movingDirection.x * speed, heroCtrl.Rigidbody.linearVelocity.y + gravity, movingDirection.z * speed);
    }
}
