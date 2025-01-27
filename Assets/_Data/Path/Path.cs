using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Path : GameMonobehaviour
{
    [SerializeField] protected List<PathPoint> points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPoint();
    }

    public virtual void LoadPoint()
    {
        if (points.Count > 0) return;
        foreach (Transform child in transform)
        {
            PathPoint point = child.GetComponent<PathPoint>();
            point.LoadNextPoint();
            points.Add(point);
        }
        Debug.LogWarning("LoadPoint", gameObject);
    }

    public virtual PathPoint GetPoint(int index)
    {
        return points[index];
    }
}
