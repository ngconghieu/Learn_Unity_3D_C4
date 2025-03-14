using System;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] protected List<FollowPath> paths;

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
            FollowPath path = child.GetComponent<FollowPath>();
            path.LoadPoint();
            paths.Add(path);
        }
        Debug.LogWarning("LoadPath", gameObject);
    }

    public virtual FollowPath GetPath(int index)
    {
        return paths[index];
    }

    public virtual FollowPath GetPath(string pathName)
    {
        foreach (FollowPath path in paths)
            if (path.name == pathName) return path;
        return null;
    }
}
