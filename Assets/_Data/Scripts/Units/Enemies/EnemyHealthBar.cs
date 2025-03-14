using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : SliderHP
{
    [SerializeField] private EnemyCtrl EnemyCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }

    private void LoadEnemyCtrl()
    {
        if (EnemyCtrl != null) return;
        EnemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.LogWarning("LoadEnemyCtrl", gameObject);
    }

    public override float GetValue()
    {
        return (float)EnemyCtrl.DmgReceiver.CurrentHP / (float)EnemyCtrl.DmgReceiver.MaxHP;
    }


}
