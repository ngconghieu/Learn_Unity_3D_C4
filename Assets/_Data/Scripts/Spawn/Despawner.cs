using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : MonoBehaviour
{
    public virtual void Despawn(T prefab)
    {
        Spawner<T>.Instance.Despawn(prefab);
    }
}