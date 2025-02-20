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
            newObject = Instantiate(prefab, position, rotation);
        }
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
            this.AddObjectToPool(obj);
        }
    }

    protected virtual void AddObjectToPool(T obj)
    {
        this.poolObj.Add(obj);
    }

    protected virtual void RemoveObjectFromPool(T obj)
    {
        this.poolObj.Remove(obj);
    }

    protected virtual T GetObjFromPool(T prefab)
    {
        foreach (T inPoolObj in this.poolObj)
        {
            if (prefab.name == inPoolObj.name)
            {
                this.RemoveObjectFromPool(inPoolObj);
                return inPoolObj;
            }
        }

        return null;
    }
}