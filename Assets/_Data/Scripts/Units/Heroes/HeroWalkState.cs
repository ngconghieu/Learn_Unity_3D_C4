using UnityEngine;

public class HeroWalkState : BaseState<State>
{
    private HeroCtrl _heroCtrl;
    [SerializeField] protected float walkingSpeed = 5f;
    [SerializeField] protected float runningSpeedMultiplier = 1.8f;
    [SerializeField] protected float rotateSpeed = 8;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isRunning = false;
    protected Vector3 _movingDirection;
    private Vector2 _movingInput;
    private Quaternion _yAxis;

    public HeroWalkState(HeroCtrl owner) : base(owner)
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
        isWalking = InputManager.Instance.IsWalking;
        isRunning = InputManager.Instance.IsRunning;
    }

    private void AnimationHandling()
    {
        _heroCtrl.Animator.SetBool(Const.IsRunning, isRunning);
        _heroCtrl.Animator.SetBool(Const.IsWalking, isWalking);
    }

    private void CheckMoving()
    {
        if (!isWalking) return;
        _movingInput = InputManager.Instance.MoveInput;
        //Get rotation of hero though camera
        _yAxis = Quaternion.Euler(0, _heroCtrl.CameraCtrl.ControlRotation.y, 0);
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
        _heroCtrl.transform.rotation = Quaternion.Slerp(
            _heroCtrl.transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed);
    }

    private void Moving(float speed)
    {
        _heroCtrl.CharacterController.Move(speed * Time.deltaTime * _movingDirection);
    }
}
