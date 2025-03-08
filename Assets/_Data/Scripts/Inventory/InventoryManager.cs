using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<InventoryCtrl> _inventories = new();
    public List<InventoryCtrl> Inventories => _inventories;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInventory();
    }

    private void LoadInventory()
    {
        if (_inventories.Count > 0) return;
        InventoryCtrl[] inventories = GetComponentsInChildren<InventoryCtrl>();
        foreach (InventoryCtrl inventory in inventories)
        {
            _inventories.Add(inventory);
        }
        Debug.LogWarning("LoadInventory", gameObject);
    }
}
