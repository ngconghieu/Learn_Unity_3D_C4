using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : GameMonobehaviour
{
    public GameObject target;
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected int pathIndex = 0;
    [SerializeField] protected Path enemyPath;

    private void FixedUpdate()
    {
        Moving();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
        LoadTargetMoving();
        LoadEnemyPath();

    }

    protected virtual void Moving()
    {
        enemyCtrl.Agent.SetDestination(target.transform.position);
    }

    private void LoadTargetMoving()
    {
        if (target != null) return;
        target = GameObject.Find("TargetMoving");
        Debug.LogWarning("LoadTargetMoving", gameObject);
    }

    private void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.LogWarning("LoadEnemyCtrl", gameObject);
    }

    protected virtual void LoadEnemyPath()
    {
        if (enemyPath != null) return;
        enemyPath = PathManager.Instance.GetPath(pathIndex);
        Debug.LogWarning("LoadEnemyPath", gameObject);
    }
}
