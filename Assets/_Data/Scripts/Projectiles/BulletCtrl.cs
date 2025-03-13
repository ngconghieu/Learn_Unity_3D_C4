using System;
using UnityEngine;

public class BulletCtrl : GameMonoBehaviour
{
    [SerializeField] protected float speed = 30f;
    [SerializeField] protected Despawner<BulletCtrl> despawnBullet;
    public Despawner<BulletCtrl> DespawnBullet => despawnBullet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnBullet();
    }

    private void LoadDespawnBullet()
    {
        if (despawnBullet != null) return;
        despawnBullet = GetComponentInChildren<Despawner<BulletCtrl>>();
        Debug.LogWarning("LoadDespawnBullet", gameObject);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}