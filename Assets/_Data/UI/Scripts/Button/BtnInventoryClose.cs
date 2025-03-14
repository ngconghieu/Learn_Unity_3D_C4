using UnityEngine;

public class BtnInventoryClose : BtnAbstract
{
    public virtual void CloseInventory()
    {
        InventoryUI.Instance.Hide();
    }

    protected override void OnClick()
    {
        CloseInventory();
    }
}
