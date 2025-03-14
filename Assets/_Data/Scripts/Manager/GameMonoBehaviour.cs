using System;
using UnityEngine;

public class GameMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        ResetValue();
    }

    protected virtual void LoadComponents()
    {
        //For override
    }

    protected virtual void ResetValue()
    {
        //For override
    }
}
