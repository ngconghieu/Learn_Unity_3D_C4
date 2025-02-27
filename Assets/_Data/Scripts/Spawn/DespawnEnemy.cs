using UnityEngine;

public class DespawnEnemy : Despawner<EnemyCtrl>
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected float despawnDelay = 2f;
    [SerializeField] protected float cdTime = 0;

    #region Load Components
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
    #endregion

    public virtual void OnDespawn()
    {
        if (!enemyCtrl.DmgReceiver.CheckDead()) return;
        cdTime+= Time.deltaTime;
        if (cdTime < despawnDelay) return;
        cdTime = 0;
        Despawn(enemyCtrl);
    }

    private void Update()
    {
        OnDespawn();
    }
}
