public class GoldValue : TextAbstract
{
    private Item _item;

    private void FixedUpdate()
    {
        LoadGoldValue();
    }

    private void LoadGoldValue()
    {
        if (_item == null)
        {
            Cash cash = InventoryManager.Instance.GetInventory<Cash>();
            ItemProfiles itemProfiles = cash.GetItemProfileByItemName(ItemName.Gold);
            if (itemProfiles == null) return;
            _item = cash.GetItemByItemProfiles(itemProfiles);
            return;
        }
        string value = _item.amount.ToString();
        text.text = "Gold: " + value;
    }
}
