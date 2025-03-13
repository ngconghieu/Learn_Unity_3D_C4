using UnityEngine;

public class HeroMoveState : BaseState<State>
{
    private HeroCtrl _heroCtrl;
    protected Vector3 _movingDirection;
    private Vector2 _movingInput;
    private Quaternion _yAxis;

    public HeroMoveState(HeroCtrl owner) : base(owner)
    {
        _heroCtrl = owner;
    }

    public override State StateKey => State.Walk;

    public override void EnterState()
    {
        //Debug.Log("Entered Walk State");
    }

    public override void ExitState()
    {
        //Debug.Log("Exited Walk State");
    }

    public override State GetNextState()
    {
        bool canChangeState = InputManager.Instance.IsMovePressing;
        if(!canChangeState) return State.Idle;
        return StateKey;
    }

    public override void UpdateState()
    {
        //Debug.Log("Updating Walk State");
        CheckMovement();
        AnimationHandling();
        CheckMoving();
    }


    private void CheckMovement()
    {
        _heroCtrl.Movement.isWalking = InputManager.Instance.IsMovePressing;
        _heroCtrl.Movement.isRunning = InputManager.Instance.IsShiftPressing;
    }

    private void AnimationHandling()
    {
        _heroCtrl.Animator.SetBool(Const.IsRunning, _heroCtrl.Movement.isRunning);
        _heroCtrl.Animator.SetBool(Const.IsWalking, _heroCtrl.Movement.isWalking);
    }

    private void CheckMoving()
    {
        if (!_heroCtrl.Movement.isWalking) return;
        _movingInput = InputManager.Instance.MoveInput;
        //Get rotation of hero though camera
        _yAxis = Quaternion.Euler(0, _heroCtrl.CameraCtrl.ControlRotation.y, 0);
        Vector3 forward = _yAxis * Vector3.forward;
        Vector3 right = _yAxis * Vector3.right;
        _movingDirection = (forward * _movingInput.y + right * _movingInput.x).normalized;
        float speed = _heroCtrl.Movement.isRunning ? (_heroCtrl.Movement.runningSpeedMultiplier * _heroCtrl.Movement.walkingSpeed) : _heroCtrl.Movement.walkingSpeed;
        RotateHero();
        Moving(speed);
    }

    private void RotateHero()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_movingDirection, Vector3.up);
        _heroCtrl.transform.rotation = Quaternion.Slerp(
            _heroCtrl.transform.rotation,
            targetRotation,
            Time.deltaTime * _heroCtrl.Movement.rotateSpeed);
    }

    private void Moving(float speed)
    {
        _heroCtrl.CharacterController.Move(speed * Time.deltaTime * _movingDirection);
    }
}
