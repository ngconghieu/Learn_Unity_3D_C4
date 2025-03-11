using System;
using UnityEngine;

public class HeroStateMachine : StateMachine<State>
{
    [SerializeField] private HeroCtrl heroCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHeroCtrl();
        LoadState();
    }

    private void LoadState()
    {
        states.Add(State.Idle, new HeroIdleState(heroCtrl));
        states.Add(State.Walk, new HeroMoveState(heroCtrl));
        

        currentState = states[State.Idle];
    }

    private void LoadHeroCtrl()
    {
        if(heroCtrl != null) return;
        heroCtrl = GetComponent<HeroCtrl>();
        Debug.LogWarning("LoadHeroCtrl", gameObject);
    }
}