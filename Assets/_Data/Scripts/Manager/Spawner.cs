using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : Singleton<Spawner<T>> where T : MonoBehaviour
{
    [SerializeField] protected Transform prefabHolder;
    [SerializeField] protected List<T> poolObj = new();

    #region Load components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPrefabHolder();
    }

    private void LoadPrefabHolder()
    {
        if (prefabHolder != null) return;
        prefabHolder = transform.Find("PrefabHolder");
        if(prefabHolder == null)
            prefabHolder = new GameObject("PrefabHolder").transform;
        prefabHolder.SetParent(transform);
        Debug.LogWarning("LoadPrefabHolder", gameObject);
    }
    #endregion

    public virtual T Spawn(T prefab, Vector3 position, Quaternion rotation)
    {
        T newObject = Spawn(prefab);
        newObject.transform.SetLocalPositionAndRotation(position, rotation);
        return newObject;
    }

    public virtual T Spawn(T prefab)
    {
        T newObject = GetObjectFromPool(prefab);
        newObject.gameObject.SetActive(true);
        newObject.transform.SetParent(prefabHolder);
        newObject.name = prefab.name;
        return newObject;
    }

    public virtual void Despawn(T obj)
    {
        if (obj is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            poolObj.Add(obj);
        }
    }

    protected virtual T GetObjectFromPool(T prefab)
    {
        foreach (T obj in this.poolObj)
        {
            if (prefab.gameObject.name == obj.gameObject.name)
            {
                poolObj.Remove(obj);
                return obj;
            }
        }
        return Instantiate(prefab);
    }

    public abstract T GetItem(ItemName itemName);
}