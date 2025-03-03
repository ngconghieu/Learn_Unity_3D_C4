using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Test : GameMonoBehaviour
{
    public Transform tr;
    public Vector3 p;
    public int count = 0;
    public List<Transform> trList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        tr = transform.Find("Wall");
        p = tr.position;
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (i == 0 || i == 10 || j == 0 || j == 10)
                {
                    if (j == 5) continue;
                    Transform newWall = Instantiate(tr, new Vector3(p.x + (j * 5), p.y, p.z), Quaternion.identity);
                    newWall.SetParent(transform);
                    newWall.gameObject.SetActive(true);
                    newWall.name = "Wall_" + count;
                    count++;
                }
            }
            p = new Vector3(p.x, p.y, p.z - 5);
        }
    }
}
