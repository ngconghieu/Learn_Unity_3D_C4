using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyDmgReceiver : DmgReceiver
{
    [SerializeField] private CapsuleCollider col;
    [SerializeField] private EnemyCtrl enemyCtrl;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
        LoadEnemyCtrl();
    }

    private void LoadEnemyCtrl()
    {
        if(enemyCtrl != null) return;
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.LogWarning("LoadEnemyCtrl", gameObject);
    }

    private void LoadCollider()
    {
        if(col != null) return;
        col = GetComponent<CapsuleCollider>();
        col.isTrigger = true;
        col.center = new Vector3(0, 0.85f, 0.3f);
        col.radius = 0.7f;
        col.height = 3;
        Debug.Log("LoadCollider", gameObject);
    }
    #endregion

    protected override void IsDead()
    {
        enemyCtrl.Animator.SetTrigger(Const.isDead);
        //gameObject.SetActive(false);
    }

    protected override void IsHit()
    {
        enemyCtrl.Animator.SetTrigger(Const.isHit);
    }
}
