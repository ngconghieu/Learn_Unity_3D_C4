using UnityEngine;

public abstract class BaseState<T> where T : System.Enum
{
    public abstract T StateKey { get; }

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void UpdateState();

    public abstract T GetNextState();

    public BaseState(GameObject owner)
    {
    }
}