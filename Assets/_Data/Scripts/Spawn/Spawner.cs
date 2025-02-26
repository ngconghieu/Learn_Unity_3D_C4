using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : Singleton<Spawner<T>> where T : MonoBehaviour
{
    [SerializeField] protected List<T> poolObj = new();

    public virtual T Spawn(T prefab, Vector3 position, Quaternion rotation)
    {
        T newObject = GetObjFromPool(prefab);
        if (newObject == null)
        {
            //newObject = Instantiate(prefab, position, rotation);
            newObject = Instantiate(prefab);
        }
        newObject.transform.SetLocalPositionAndRotation(position, rotation);
        return newObject;
    }

    public virtual T Spawn(T prefab)
    {
        T newObject = GetObjFromPool(prefab) ?? Instantiate(prefab);
        return newObject;
    }

    public virtual void Despawn(Transform obj)
    {
        Destroy(obj.gameObject);
    }

    public virtual void Despawn(T obj)
    {
        if (obj is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            poolObj.Add(obj);
        }
    }

    protected virtual T GetObjFromPool(T prefab)
    {
        foreach (T obj in this.poolObj)
        {
            if (prefab.GetType() == obj.GetType())
            {
                poolObj.Remove(obj);
                return obj;
            }
        }

        return null;
    }
}