using System.Collections;
using UnityEngine;

public class TurretTowerShooting : TowerShooting
{
    [SerializeField] protected float delayEachShoot = 0.1f;
    protected override void ShootTarget()
    {
        Invoke(nameof(ShootTarget), shootSpeed);
        if (target == null) return;
        StartCoroutine(Shooting());
    }
    IEnumerator Shooting()
    {
        //spawn
        foreach (Transform firePoint in TowerCtrl.FirePoint.Points)
        {
            BulletCtrl newBullet = TowerCtrl.BulletSpawner.Spawn(TowerCtrl.Bullet,
                firePoint.position, TowerCtrl.Rotator.rotation);
            newBullet.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayEachShoot);
        }
    }
}
