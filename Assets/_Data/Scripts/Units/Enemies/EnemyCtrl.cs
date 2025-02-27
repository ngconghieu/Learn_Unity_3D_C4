using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : GameMonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected DmgReceiver dmgReceiver;
    public DmgReceiver DmgReceiver => dmgReceiver;
    [SerializeField] protected DespawnEnemy despawnEnemy;
    public DespawnEnemy DespawnEnemy => despawnEnemy;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNavMeshAgent();
        LoadAnimator();
        LoadDmgReceiver();
        LoadDespawnEnemy();
    }

    private void LoadDespawnEnemy()
    {
        if (despawnEnemy != null) return;
        despawnEnemy = GetComponentInChildren<DespawnEnemy>();
        Debug.LogWarning("LoadDespawnEnemy", gameObject);
    }

    private void LoadDmgReceiver()
    {
        if (dmgReceiver != null) return;
        dmgReceiver = GetComponentInChildren<DmgReceiver>();
        dmgReceiver.transform.localPosition = new Vector3(0, 0.7f, 0);
        Debug.LogWarning("LoadDmgReceiver", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning("LoadAnimator", gameObject);
    }

    private void LoadNavMeshAgent()
    {
        if (agent != null) return;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 4f;
        agent.angularSpeed = 200f;
        agent.acceleration = 150f;
        Debug.LogWarning("LoadNavMeshAgent", gameObject);
    }
    #endregion
}
