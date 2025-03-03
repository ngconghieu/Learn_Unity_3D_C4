using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMoving : GameMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Path enemyPath;
    [SerializeField] protected int pathName;
    [SerializeField] protected Transform currentPoint;
    [SerializeField] protected float pointDistance = Mathf.Infinity;
    [SerializeField] protected float stopDistance = 2f;
    [SerializeField] protected int pointNum = 0;
    [SerializeField] protected bool canMove = false;
    [SerializeField] protected bool isMoving = false;

    private void FixedUpdate()
    {
        Moving();
        CheckMoving();
    }

    #region Load Components
    private void Start()
    {
        LoadEnemyPath();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
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
        enemyPath = PathManager.Instance.GetPath(pathName);
        currentPoint = enemyPath.Points[pointNum];
        //Debug.LogWarning("LoadEnemyPath", gameObject);
    }
    #endregion

    private void OnEnable()
    {
        pointNum = 0;
        if (enemyPath != null)
            currentPoint = enemyPath.Points[pointNum];
    }

    protected virtual void Moving()
    {
        SetCurrentPoint();
        if (pointNum >= enemyPath.Points.Count || !canMove || enemyCtrl.DmgReceiver.CheckDead())
        {
            enemyCtrl.Agent.isStopped = true;
            return;
        }
        enemyCtrl.Agent.isStopped = false;
        enemyCtrl.Agent.SetDestination(currentPoint.position);
    }

    private void SetCurrentPoint()
    {
        pointDistance = Vector3.Distance(transform.parent.position, currentPoint.position);
        if (pointDistance < stopDistance && pointNum < enemyPath.Points.Count)
        {
            pointNum++;
            if (pointNum < enemyPath.Points.Count)
                currentPoint = enemyPath.Points[pointNum];

        }
    }

    protected virtual void CheckMoving()
    {
        if (enemyCtrl.Agent.velocity.magnitude > 0.1f)
            isMoving = true;
        else
            isMoving = false;

        enemyCtrl.Animator.SetBool(Const.IsMoving, isMoving);
    }
}
