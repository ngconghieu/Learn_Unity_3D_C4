using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCtrl : GameMonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected DmgReceiver dmgReceiver;
    [SerializeField] protected DespawnEnemy despawnEnemy;
    [SerializeField] protected EnemyMoving enemyMoving;
    public DmgReceiver DmgReceiver => dmgReceiver;
    public Animator Animator => animator;
    public NavMeshAgent Agent => agent;
    public DespawnEnemy DespawnEnemy => despawnEnemy;
    public EnemyMoving EnemyMoving => enemyMoving;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNavMeshAgent();
        LoadAnimator();
        LoadDmgReceiver();
        LoadDespawnEnemy();
        LoadEnemyMoving();
    }

    private void LoadEnemyMoving()
    {
        if (enemyMoving != null) return;
        enemyMoving = GetComponentInChildren<EnemyMoving>();
        Debug.LogWarning("LoadEnemyMoving", gameObject);
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
