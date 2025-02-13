using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Path : GameMonoBehaviour
{
    [SerializeField] protected List<Transform> points;
    public List<Transform> Points => points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPoint();
    }

    public virtual void LoadPoint()
    {
        if (points.Count > 0) return;
        foreach (Transform point in transform)
        {
            points.Add(point);
        }
        Debug.LogWarning("LoadPoint", gameObject);
    }
}
