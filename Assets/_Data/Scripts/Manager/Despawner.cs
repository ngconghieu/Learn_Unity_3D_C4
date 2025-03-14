using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : MonoBehaviour
{
    public virtual void Despawn(Transform obj)
    {
        Destroy(obj.gameObject);
    }

    public virtual void Despawn(T prefab)
    {
        Spawner<T>.Instance.Despawn(prefab);
    }
}