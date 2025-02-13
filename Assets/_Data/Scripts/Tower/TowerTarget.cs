using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class TowerTarget : GameMonoBehaviour
{
    [SerializeField] protected SphereCollider cl;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected List<EnemyCtrl> enemies = new();
    [SerializeField] protected EnemyCtrl target;
    public EnemyCtrl Target => target;

    #region Load components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
        LoadRigibody();
    }

    private void LoadRigibody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.LogWarning("LoadRigibody", gameObject);
    }

    private void LoadCollider()
    {
        if (cl != null) return;
        cl = GetComponent<SphereCollider>();
        cl.isTrigger = true;
        cl.radius = 15f;
        Debug.LogWarning("LoadCollider", gameObject);
    }
    #endregion

    private void FixedUpdate()
    {
        FocusTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        AddEnemy(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveEnemy(other);
    }

    protected virtual void AddEnemy(Collider collider)
    {
        if(collider.TryGetComponent<EnemyCtrl>(out EnemyCtrl component))
        {
            enemies.Add(component);
        }
    }

    protected virtual void RemoveEnemy(Collider collider)
    {
        foreach(EnemyCtrl enemy in enemies)
        {
            if (collider.gameObject.Equals(enemy.gameObject))
            {
                enemies.Remove(enemy);
                return;
            }
        }
    }
    protected virtual void FocusTarget()
    {
        if (enemies.Count != 0)
            target = enemies[0];
        else
            target = null;
    }
}
