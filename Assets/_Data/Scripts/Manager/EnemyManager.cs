using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Spawner<EnemyCtrl>
{
    [SerializeField] protected List<EnemyCtrl> enemies = new();
    [SerializeField] protected float spawnDelay = 2f;
    [SerializeField] protected int maxEnemy = 10;
    [SerializeField] protected int enemyCount = 0;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemies();
    }

    private void LoadEnemies()
    {
        if (enemies.Count > 0) return;
        foreach (EnemyCtrl enemy in GetComponentsInChildren<EnemyCtrl>())
        {
            enemies.Add(enemy);
            enemy.gameObject.SetActive(false);
        }
    }
    #endregion

    protected virtual void Start()
    {
        Invoke(nameof(SpawnEnemies), spawnDelay);
    }

    protected virtual void SpawnEnemies()
    {
        Invoke(nameof(SpawnEnemies), spawnDelay);
        if (enemyCount >= maxEnemy) return;
        int random = UnityEngine.Random.Range(0, enemies.Count);
        EnemyCtrl newEnemy = Spawn(enemies[random]);
        newEnemy.transform.SetLocalPositionAndRotation(
            PathManager.Instance.GetPath(newEnemy.EnemyMoving.PathNum).Points[0].position,
            Quaternion.identity);
        enemyCount++;
    }

    public override EnemyCtrl GetItem(ItemName itemName)
    {
        return enemies.Find(enemy => enemy.name == itemName.ToString());
    }
}
