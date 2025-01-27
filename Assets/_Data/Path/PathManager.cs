using System;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] protected List<Path> paths;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPath();
    }

    private void LoadPath()
    {
        if (paths.Count > 0) return;
        foreach (Transform child in transform)
        {
            Path path = child.GetComponent<Path>();
            path.LoadPoint();
            paths.Add(path);
        }
        Debug.LogWarning("LoadPath", gameObject);
    }

    public virtual Path GetPath(int index)
    {
        return paths[index];
    }

    public virtual Path GetPath(string pathName)
    {
        foreach (Path path in paths)
            if (path.name == pathName) return path;
        return null;
    }
}
