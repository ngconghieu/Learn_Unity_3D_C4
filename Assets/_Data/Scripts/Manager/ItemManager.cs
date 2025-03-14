using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Spawner<ItemCtrl>
{
    [SerializeField] protected List<ItemCtrl> items;

    public override ItemCtrl GetItem(ItemName itemName)
    {
        return items.Find(item => item.name == itemName.ToString());
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItems();
    }

    private void LoadItems()
    {
        if (items.Count > 0) return;
        foreach (ItemCtrl item in GetComponentsInChildren<ItemCtrl>())
        {
            item.gameObject.SetActive(false);
            items.Add(item);
        }
        Debug.LogWarning("LoadItems", gameObject);
    }

    
}
