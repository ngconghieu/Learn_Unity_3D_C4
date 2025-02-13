using System;
using UnityEngine;

public abstract class TowerAbstract : GameMonoBehaviour
{
    [SerializeField] private TowerCtrl _towerCtrl;
    public TowerCtrl TowerCtrl => _towerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTowerCtrl();
    }

    private void LoadTowerCtrl()
    {
        if (_towerCtrl != null) return;
        _towerCtrl = GetComponentInParent<TowerCtrl>();
        Debug.LogWarning("LoadTowerCtrl", gameObject);
    }
}
