using UnityEngine;

public class IdleState : BaseState<State>
{
    private float idleTime = 0f;
    private float maxIdleTime = 5f;
    public override State StateKey => State.Idle;

    public IdleState(GameObject owner) : base(owner) { }

    public override void EnterState()
    {
        Debug.Log("Entered Idle State");
    }

    public override void ExitState()
    {
        Debug.Log("Exited Idle State");
    }

    public override void UpdateState()
    {
        Debug.Log("Updating Idle State");
        idleTime += Time.deltaTime;
    }

    public override State GetNextState()
    {
        if(idleTime > maxIdleTime) return State.Walk;
        return StateKey;
    }
}