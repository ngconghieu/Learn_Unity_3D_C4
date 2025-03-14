using UnityEngine;

public class HeroIdleState : BaseState<State>
{

    public override State StateKey => State.Idle;

    public HeroIdleState(HeroCtrl owner) : base(owner) { }

    public override void EnterState()
    {
        //Debug.Log("Entered Idle State");
    }

    public override void ExitState()
    {
        //Debug.Log("Exited Idle State");
    }

    public override void UpdateState()
    {
        //Debug.Log("Updating Idle State");
    }

    public override void FixedUpdateState()
    {
        
    }

    public override State GetNextState()
    {
        bool canChangeState = InputManager.Instance.IsWalking;
        if(canChangeState)
        {
            return State.Walk;
        }
        return StateKey;
    }
}