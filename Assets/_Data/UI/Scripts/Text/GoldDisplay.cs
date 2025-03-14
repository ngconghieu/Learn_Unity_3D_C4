using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDisplay : TextAbstract
{
    private List<Item> _items;
    private Cash _cash;
    private ItemProfiles itemProfiles;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        LoadItem();
    }

    private void OnEnable()
    {
        if (_cash == null) return;
        _cash.OnInventoryChanged += LoadGoldDisplay;
    }

    private void OnDisable()
    {
        _cash.OnInventoryChanged -= LoadGoldDisplay;
    }

    private void LoadItem()
    {
        _cash = InventoryManager.Instance.GetInventory<Cash>();
        itemProfiles = _cash.GetItemProfileByItemName(ItemName.Gold);
        _items = _cash.GetItems();
        _cash.OnInventoryChanged += LoadGoldDisplay;
    }

    private void LoadGoldDisplay()
    {
        if(_items.Count == 0) return;
        int amount = 0;
        foreach (Item item in _items)
            amount += item.amount;
        text.text = "Gold: " + amount.ToString();
    }
}
