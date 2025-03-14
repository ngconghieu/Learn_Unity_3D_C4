using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : Despawner<BulletCtrl>
{
    [SerializeField] protected BulletCtrl bullet;
    [SerializeField] protected float cdTime = 0;
    [SerializeField] protected float DespawnTime = 7f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBullet();
    }

    private void LoadBullet()
    {
        if (bullet != null) return;
        bullet = GetComponentInParent<BulletCtrl>();
        Debug.LogWarning("LoadBullet", gameObject);
    }

    private void FixedUpdate()
    {
        CheckDespawn();
    }

    protected virtual void CheckDespawn()
    {
        cdTime += Time.fixedDeltaTime;
        if (cdTime < DespawnTime) return;
        Despawn(bullet);
    }

    public override void Despawn(BulletCtrl prefab)
    {
        cdTime = 0;
        base.Despawn(prefab);
    }
}