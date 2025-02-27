using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : Singleton<Spawner<T>> where T : MonoBehaviour
{
    [SerializeField] protected List<T> poolObj = new();
    [SerializeField] protected Transform prefabHolder;

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
        Debug.LogWarning("LoadPrefabHolder", gameObject);
    }
    #endregion

    public virtual T Spawn(T prefab, Vector3 position, Quaternion rotation)
    {
        T newObject = Spawn(prefab);
        newObject.transform.SetLocalPositionAndRotation(position, rotation);
        newObject.gameObject.SetActive(true);
        newObject.transform.SetParent(prefabHolder);
        newObject.name = prefab.name;
        return newObject;
    }

    public virtual T Spawn(T prefab)
    {
        T newObject = GetObjFromPool(prefab) ?? Instantiate(prefab);
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

    protected virtual T GetObjFromPool(T prefab)
    {
        foreach (T obj in this.poolObj)
        {
            //if (prefab.GetType() == obj.GetType())
            if (prefab.gameObject.name == obj.gameObject.name)
            {
                poolObj.Remove(obj);
                return obj;
            }
        }
        return null;
    }
}