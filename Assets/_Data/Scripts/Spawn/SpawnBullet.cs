using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : Spawner<BulletCtrl>
{
    [SerializeField] protected BulletCtrl bullet;
    public BulletCtrl Bullet => bullet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBullet();
    }

    private void LoadBullet()
    {
        if (bullet != null) return;
        bullet = GetComponentInChildren<BulletCtrl>();
        bullet.gameObject.SetActive(false);
        Debug.LogWarning("LoadBullet", gameObject);
    }
}