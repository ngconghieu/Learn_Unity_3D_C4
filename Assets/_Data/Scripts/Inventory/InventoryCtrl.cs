using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryCtrl : GameMonoBehaviour
{
    [SerializeField] protected List<Item> items = new();

    protected virtual void AddItem(Item item)
    {
        items.Add(item);
    }
}

//item
[Serializable]
public struct Item
{
    public ItemName name;
    public int amount;
}

//item name
public enum ItemName
{
    None = 0,
    Cash = 1,
    Sword = 2,
}
