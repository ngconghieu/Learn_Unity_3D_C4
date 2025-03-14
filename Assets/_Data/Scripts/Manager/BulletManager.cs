using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Spawner<BulletCtrl>
{
    [SerializeField] protected List<BulletCtrl> bullets;

    public override BulletCtrl GetItem(ItemName itemName)
    {
        return bullets.Find(item => item.name == itemName.ToString());
    }
}