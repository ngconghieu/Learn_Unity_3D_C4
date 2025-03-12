using System.Collections;
using UnityEngine;

public class GoldDisplay : TextAbstract
{
    private Item _item;
    private Cash _cash;
    private ItemProfiles itemProfiles;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
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
        _item = _cash.GetItemByItemProfiles(itemProfiles);
        _cash.OnInventoryChanged += LoadGoldDisplay;
    }

    private void LoadGoldDisplay()
    {
        _item ??= _cash.GetItemByItemProfiles(itemProfiles);
        if (_item != null)
            text.text = "Gold: " + _item.amount.ToString();
    }
}
