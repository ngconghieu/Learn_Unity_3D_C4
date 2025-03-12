using System;
using TMPro;
using UnityEngine;

public class BtnItem : BtnAbstract
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _text;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }

    private void LoadText()
    {
        if (_text != null) return;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    #endregion

    public int GetAmount()
    {
        return Int32.TryParse(_text.text, out int amount) ? amount : 0;
    }
    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetAmount(int text)
    {
        _text.text = text < 2 ? string.Empty : text.ToString();
    }

    protected override void OnClick()
    {
        Debug.Log("Item Clicked");
    }
}
