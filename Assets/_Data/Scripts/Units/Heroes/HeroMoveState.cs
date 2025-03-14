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
        bool canChangeState = InputManager.Instance.IsWalking;
        if (!canChangeState) return State.Idle;
        return StateKey;
    }

    public override void UpdateState()
    {
        //Debug.Log("Updating Walk State");
        CheckMovement();
        AnimationHandling();
        CheckMoving();
        Moving();
    }


    private void CheckMovement()
    {
        _heroCtrl.Movement.isWalking = InputManager.Instance.IsWalking;
        _heroCtrl.Movement.isRunning = InputManager.Instance.IsRunning;
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

        _yAxis = Quaternion.Euler(0, _heroCtrl.CameraCtrl.ControlRotation.y, 0); //Get the rotation of the hero through the camera
        Vector3 forward = _yAxis * Vector3.forward;
        Vector3 right = _yAxis * Vector3.right;
        _movingDirection = (forward * _movingInput.y + right * _movingInput.x).normalized;
        RotateHero();
    }

    private void RotateHero()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_movingDirection, Vector3.up);
        _heroCtrl.transform.rotation = Quaternion.Slerp(
            _heroCtrl.transform.rotation,
            targetRotation,
            Time.deltaTime * _heroCtrl.Movement.rotateSpeed);
    }

    public override void FixedUpdateState()
    {
        //Moving();
    }

    private void Moving()
    {
        if (_movingInput == Vector2.zero)
        {
            _heroCtrl.Rigidbody.linearVelocity = Vector3.zero;
            return;
        }
        float speed = _heroCtrl.Movement.isRunning ? (_heroCtrl.Movement.runningSpeedMultiplier * _heroCtrl.Movement.walkingSpeed) : _heroCtrl.Movement.walkingSpeed;
        _heroCtrl.Rigidbody.MovePosition(_heroCtrl.transform.position + speed * Time.fixedDeltaTime * _movingDirection);
    }
}
