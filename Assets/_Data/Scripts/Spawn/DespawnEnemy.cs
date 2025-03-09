using System;
using UnityEngine;

public class DespawnEnemy : Despawner<EnemyCtrl>
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected float despawnDelay = 2f;
    [SerializeField] protected float cdTime = 0;
    private bool _isDropItem = false;

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

    private void OnEnable()
    {
        _isDropItem = false;
    }

    private void Update()
    {
        OnDespawn();
    }

    public virtual void OnDespawn()
    {
        if (!enemyCtrl.DmgReceiver.CheckDead()) return;
        if (!_isDropItem)
        {
            DropItem();
            _isDropItem = true;
        }

        cdTime += Time.deltaTime;
        if (cdTime < despawnDelay) return;
        cdTime = 0;
        Despawn(enemyCtrl);
    }

    private void DropItem()
    {
        InventoryManager.Instance.GetInventory<Cash>().AddItem(ItemName.Gold, 2);
    }

}
