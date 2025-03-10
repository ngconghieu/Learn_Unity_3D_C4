using UnityEngine;

public class PlayerStateManager : StateMachine<State>
{
    public GameObject player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        states.Add(State.Idle, new IdleState(player));
        states.Add(State.Walk, new WalkState(player));
        // Add other states

        currentState = states[State.Idle];
    }
}