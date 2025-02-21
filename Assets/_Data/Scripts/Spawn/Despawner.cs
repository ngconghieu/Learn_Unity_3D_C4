using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T parent;
    [SerializeField] protected float cdTime = 0;
    [SerializeField] protected float DespawnTime = 7f;

    protected virtual void FixedUpdate()
    {
        CheckDespawn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParent();
    }

    protected virtual void LoadParent()
    {
        if (this.parent != null) return;
        this.parent = transform.parent.GetComponent<T>();
        Debug.LogWarning("LoadParent", gameObject);
    }

    protected virtual void CheckDespawn()
    {
        cdTime += Time.fixedDeltaTime;
        if (cdTime < DespawnTime) return;
        Spawner<T>.Instance.Despawn(parent);
        cdTime = 0;
    }
}