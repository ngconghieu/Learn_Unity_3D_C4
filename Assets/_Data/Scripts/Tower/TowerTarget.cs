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
        RemoveDeadEnemy();
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
        if (collider.transform.parent.TryGetComponent<EnemyCtrl>(out EnemyCtrl component))
        {
            if (component.DmgReceiver.CheckDead()) return;
            enemies.Add(component);
        }
    }

    protected virtual void RemoveEnemy(Collider collider)
    {
        foreach (EnemyCtrl enemy in enemies)
        {
            if (collider.transform.parent.gameObject.Equals(enemy.gameObject))
            {
                enemies.Remove(enemy);
                return;
            }
        }
    }

    protected virtual void RemoveDeadEnemy()
    {
        foreach (EnemyCtrl enemy in enemies)
        {
            if (enemy.DmgReceiver.CheckDead())
            {
                enemies.Remove(enemy);
                return;
            }
        }
    }

    protected virtual void FocusTarget()
    {
        if (enemies.Count == 0)
        {
            target = null;
            return;
        }
        //Show raycast to all enemies
        foreach (EnemyCtrl enemy in enemies)
            DetectEnemy(enemy);
        target = enemies.Find(enemy => DetectEnemy(enemy));
    }

    private bool DetectEnemy(EnemyCtrl enemy)
    {
        Vector3 direction = enemy.DmgReceiver.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, direction.magnitude))
        {
            //Debug.Log(hit.transform.parent.gameObject.name);
            bool canSeeEnemy = hit.transform.parent.gameObject.Equals(enemy.gameObject);
            Debug.DrawRay(transform.position, direction, canSeeEnemy ? Color.green : Color.red);
            return canSeeEnemy;
        }
        return false;
    }
}
