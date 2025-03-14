using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : GameMonoBehaviour where T: Enum
{
    protected Dictionary<T, BaseState<T>> states = new();
    protected BaseState<T> currentState;
    protected bool isChangingState;

    public T CurrentStateKey => currentState.StateKey;

    public void Start()
    {
        currentState.EnterState();
    }

    public void Update()
    {
        currentState?.UpdateState();
        T nextStateKey = currentState.GetNextState();
        if (!nextStateKey.Equals(currentState.StateKey))
        {
            ChangeState(nextStateKey);
        }
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }

    private void ChangeState(T nextStateKey)
    {
        if(isChangingState)
        {
            return;
        }
        isChangingState = true;
        currentState?.ExitState();
        currentState = states[nextStateKey];
        currentState.EnterState();

        isChangingState = false;
    }
}
