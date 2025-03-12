using UnityEngine;

public abstract class EnemyAbstract : GameMonoBehaviour
{
    [SerializeField] private EnemyCtrl _enemyCtrl;
    public EnemyCtrl EnemyCtrl => _enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }

    private void LoadEnemyCtrl()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.LogWarning("LoadEnemyCtrl", gameObject);
    }
}
