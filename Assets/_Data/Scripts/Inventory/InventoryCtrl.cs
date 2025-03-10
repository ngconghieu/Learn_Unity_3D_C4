using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class InventoryCtrl : GameMonoBehaviour
{
    [SerializeField] protected AssetLabelReference label;
    [SerializeField] protected List<ItemProfiles> itemProfiles = new();
    [SerializeField] private List<Item> items = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemProfiles();
    }

    protected abstract void LoadItemProfiles();

    public virtual Item GetItemByItemProfiles(ItemProfiles itemProfiles) 
    {
        if(itemProfiles == null || items == null) return null;
        return items.Find(items => items.itemProfiles == itemProfiles);
    }

    public virtual void AddItem(ItemName itemName, int amount)
    {
        ItemProfiles itemProfiles = GetItemProfileByItemName(itemName);
        int amountToStack = amount;

        foreach (Item item in items) // fill up existing stacks
        {
            if (item.itemProfiles != itemProfiles || item.amount == item.itemProfiles.maxStack) continue;
            StackableItems(item, ref amountToStack);
        }

        while (amountToStack > 0)
        {
            int amountToAdd = Mathf.Min(amountToStack, itemProfiles.maxStack);
            Item item = new()
            {
                itemProfiles = itemProfiles,
                amount = amountToAdd
            };
            items.Add(item);
            amountToStack -= amountToAdd;
        }
    }

    protected virtual bool ItemExists(ItemProfiles itemProfiles) //check existance of item
    {
        Item item = items.Find(items => items.itemProfiles == itemProfiles);
        if (item == null)
            return false;
        return true;
    }

    public virtual ItemProfiles GetItemProfileByItemName(ItemName itemName)
    {
        foreach (ItemProfiles itemProfile in itemProfiles)
        {
            if (itemProfile.ItemName == itemName)
            {
                return itemProfile;
            }
        }
        return null;
    }

    protected virtual void StackableItems(Item item, ref int amount)
    {
        int AmountNeededToMaxStack = item.itemProfiles.maxStack - item.amount;
        if (AmountNeededToMaxStack >= amount)
        {
            item.amount += amount;
            amount = 0;
        }
        else
        {
            item.amount += AmountNeededToMaxStack;
            amount -= AmountNeededToMaxStack;
        }
    }
}

//item
[Serializable]
public class Item
{
    public ItemProfiles itemProfiles;
    public int amount;
}
