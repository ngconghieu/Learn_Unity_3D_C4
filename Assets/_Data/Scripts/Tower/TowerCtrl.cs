using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerCtrl : GameMonoBehaviour
{
    [SerializeField] protected Transform model;
    
    [SerializeField] protected Transform rotator;
    public Transform Rotator => rotator;
    
    [SerializeField] protected TowerTarget towerTarget;
    public TowerTarget TowerTarget => towerTarget;
    
    [SerializeField] protected SpawnBullet bulletSpawner;
    public SpawnBullet BulletSpawner => bulletSpawner;

    [SerializeField] protected BulletCtrl bullet;
    public BulletCtrl Bullet => bullet;

    [SerializeField] protected SpawnBullet spawnBullet;
    public SpawnBullet SpawnBullet => SpawnBullet;

    [SerializeField] protected FirePoint firePoint;
    public FirePoint FirePoint => firePoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadTowerTarget();
        LoadBulletSpawner();
        LoadSpawnBullet();
        LoadBullet();
        LoadFirePoint();
    }

    private void LoadSpawnBullet()
    {
        if (spawnBullet != null) return;
        spawnBullet = GameObject.FindFirstObjectByType<SpawnBullet>();
        Debug.LogWarning("LoadSpawnBullet", gameObject);
    }

    private void LoadFirePoint()
    {
        if (firePoint != null) return;
        firePoint = Rotator.GetComponentInChildren<FirePoint>();
        Debug.LogWarning("LoadFirePoint", gameObject);

    }

    private void LoadBullet()
    {
        if (bullet != null) return;
        bullet = spawnBullet.Bullet;
        Debug.LogWarning("LoadBullet", gameObject);
    }

    private void LoadBulletSpawner()
    {
        if (bulletSpawner != null) return;
        bulletSpawner = FindFirstObjectByType<SpawnBullet>();
        Debug.LogWarning("LoadBulletSpawner", gameObject);
    }

    private void LoadTowerTarget()
    {
        if (towerTarget != null) return;
        towerTarget = GetComponentInChildren<TowerTarget>();
        Debug.LogWarning("LoadTowerTarget", gameObject);
    }

    private void LoadModel()
    {
        if (model != null) return;
        model = transform.Find("Model");
        rotator = model.Find("Rotator");
        Debug.LogWarning("LoadModel", gameObject);
    }
}
