using System;
using UnityEngine;

public class BulletDmgSender : DmgSender
{
    [SerializeField] protected BulletCtrl bullet;

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

    protected override void SendDmg(DmgReceiver dmgReceiver)
    {
        base.SendDmg(dmgReceiver);
        bullet.DespawnBullet.Despawn(bullet);

    }
}
