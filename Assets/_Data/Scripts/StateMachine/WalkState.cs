using UnityEngine;

public class WalkState : BaseState<State>
{
    public WalkState(GameObject owner) : base(owner)
    {
    }

    public override State StateKey => State.Walk;

    public override void EnterState()
    {
        Debug.Log("Entered Walk State");
    }

    public override void ExitState()
    {
        Debug.Log("Exited Walk State");
    }

    public override State GetNextState()
    {
        return StateKey;
    }

    public override void UpdateState()
    {
        Debug.Log("Updating Walk State");
    }
}
