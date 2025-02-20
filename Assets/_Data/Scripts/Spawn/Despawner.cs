using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T parent;
    [SerializeField] protected float timeLife = 7f;
    [SerializeField] protected float currentTime = 7f;

    protected virtual void FixedUpdate()
    {
        this.DespawnChecking();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParent();
    }

    protected virtual void LoadParent()
    {
        if (this.parent != null) return;
        this.parent = transform.parent.GetComponent<T>();
        Debug.Log("LoadParent", gameObject);
    }

    protected virtual void DespawnChecking()
    {
        this.currentTime -= Time.fixedDeltaTime;
        if (this.currentTime > 0) return;

        Spawner<T>.Instance.Despawn(this.parent);
        this.currentTime = this.timeLife;
    }
}