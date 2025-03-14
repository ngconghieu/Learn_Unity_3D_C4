using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class InventoryCtrl : GameMonoBehaviour
{
    [SerializeField] protected AssetLabelReference label;
    [SerializeField] protected List<ItemProfiles> itemProfiles = new();
    [SerializeField] private List<Item> items = new();
    [HideInInspector] public event Action OnInventoryChanged;

    private void Start()
    {
        LoadItemProfiles();
    }

    protected abstract void LoadItemProfiles();

    public virtual Item GetItemByItemProfiles(ItemProfiles itemProfiles)
    {
        if (itemProfiles == null || items == null) return null;
        return items.Find(items => items.itemProfiles == itemProfiles);
    }

    public virtual List<Item> GetItems() => items;

    public virtual void AddItem(ItemName itemName, int amount)
    {
        ItemProfiles itemProfiles = GetItemProfileByItemName(itemName);
        if(itemProfiles == null) return;
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
                itemID = Guid.NewGuid().ToString(),
                itemProfiles = itemProfiles,
                amount = amountToAdd
            };
            items.Add(item);
            amountToStack -= amountToAdd;
        }
        NotifyInventoryChanged();
    }

    public virtual void RemoveItem(ItemName itemName, int amount)
    {
        if (amount <= 0) return;
        if (!CheckItemAmountToRemove(itemName, amount)) return;
        int remainingAmount = amount;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemProfiles.ItemName != itemName)
                continue;
            if (remainingAmount <= items[i].amount)
            {
                items[i].amount -= remainingAmount;
                break;
            }
            remainingAmount -= items[i].amount; //
            items[i].amount = 0;
        }
        NotifyInventoryChanged();
        items.RemoveAll(item => item.amount == 0);
    }

    public void RemoveItem(Item item) =>
        items.Remove(item);

    public virtual bool CheckItemAmountToRemove(ItemName itemName, int amount)
    {
        int consumption = 0;
        for (int _item = items.Count - 1; _item >= 0; _item--)
        {
            if (items[_item].itemProfiles.ItemName != itemName)
                continue;
            consumption += items[_item].amount;
            if (consumption >= amount) return true;
        }
        return false;
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

    private void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }
}

//item
[Serializable]
public class Item
{
    public string itemID;
    public ItemProfiles itemProfiles;
    public int amount;
}
