using System;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : GameMonoBehaviour
{
    [SerializeField] private List<Transform> points = new();
    public List<Transform> Points => points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPoints();
    }

    private void LoadPoints()
    {
        if (points.Count > 0) return;
        foreach (Transform point in transform)
            points.Add(point);
        Debug.LogWarning("LoadPoints", gameObject);
    }
}
