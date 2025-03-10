using System;
using UnityEngine;

public class TowerShooting : TowerAbstract
{
    [SerializeField] protected EnemyCtrl target;
    [SerializeField] protected float rotationSpeed = 2f;
    [SerializeField] protected float shootSpeed = 1f;

    private void Start()
    {
        Invoke(nameof(ShootTarget), shootSpeed);
    }

    protected void FixedUpdate()
    {
        LoadTarget();
        LookAtTarget();
    }

    protected virtual void ShootTarget()
    {
        Invoke(nameof(ShootTarget), shootSpeed);
        if (target == null) return;
        //spawn
        foreach (Transform firePoint in TowerCtrl.FirePoint.Points)
        {
            BulletCtrl newBullet = TowerCtrl.BulletSpawner.Spawn(TowerCtrl.Bullet,
                firePoint.position, TowerCtrl.Rotator.rotation);
            newBullet.gameObject.SetActive(true);
        }
    }

    private void LoadTarget()
    {
        target = TowerCtrl.TowerTarget.Target;
    }

    private void LookAtTarget()
    {
        if (target == null) return;
        Vector3 dir = target.DmgReceiver.transform.position - TowerCtrl.Rotator.position;
        Vector3 newDir = Vector3.RotateTowards(TowerCtrl.Rotator.forward, dir, rotationSpeed * Time.fixedDeltaTime, 0f);
        TowerCtrl.Rotator.rotation = Quaternion.LookRotation(newDir);
    }
}
