using System;
using UnityEngine;

public abstract class TowerShooting : TowerAbstract
{
    [SerializeField] protected EnemyCtrl target;
    [SerializeField] protected float rotationSpeed = 2f;
    [SerializeField] protected float shootSpeed = 1f;

    protected void FixedUpdate()
    {
        LoadTarget();
    }

    protected abstract void ShootTarget();

    private void LoadTarget()
    {
        target = TowerCtrl.TowerTarget.Target;
        if (target == null) return;
        LookAtTarget();
        ShootTarget();
    }

    private void LookAtTarget()
    {
        Vector3 dir = target.DmgReceiver.transform.position - TowerCtrl.Rotator.position;
        Vector3 newDir = Vector3.RotateTowards(TowerCtrl.Rotator.forward, dir, rotationSpeed * Time.fixedDeltaTime, 0f);
        TowerCtrl.Rotator.rotation = Quaternion.LookRotation(newDir);
    }
}
