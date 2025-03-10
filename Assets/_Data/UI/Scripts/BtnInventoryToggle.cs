using UnityEngine;

public class BtnInventoryToggle : BtnAbstract
{
    protected override void OnClick()
    {
        InventoryUI.Instance.Toggle();
    }
}
