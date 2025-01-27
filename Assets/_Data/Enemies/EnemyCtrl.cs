using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : GameMonobehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNavMeshAgent();
    }

    private void LoadNavMeshAgent()
    {
        if (agent != null) return;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2f;
        agent.angularSpeed = 200f;
        agent.acceleration = 150f;
        Debug.LogWarning("LoadNavMeshAgent", gameObject);
    }
}
