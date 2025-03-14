using System.Collections;
using UnityEngine;

public class TurretTowerShooting : TowerShooting
{
    [SerializeField] protected float delayEachShoot = 0.1f;
    private float _timer = 0;
    protected override void ShootTarget()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer < shootSpeed) return;
        StartCoroutine(Shooting());
        _timer = 0;
    }
    IEnumerator Shooting()
    {
        //spawn
        foreach (Transform firePoint in TowerCtrl.FirePoint.Points)
        {
            BulletCtrl newBullet = BulletManager.Instance.Spawn(
                TowerCtrl.Bullet,
                firePoint.position, 
                TowerCtrl.Rotator.rotation);
            newBullet.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayEachShoot);
        }
    }
}
