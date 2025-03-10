using System;
using UnityEngine;

public abstract class Singleton<T> : GameMonoBehaviour where T : GameMonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance;

    protected override void Awake()
    {
        LoadInstance();
    }

    private void LoadInstance()
    {
        if (_instance != null)
        {
            Debug.LogError("Singleton already exists", gameObject);
            return;
        }
        _instance = this as T;
        //DontDestroyOnLoad(gameObject);
    }
}