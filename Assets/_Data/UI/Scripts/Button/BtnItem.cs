using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnItem : BtnAbstract
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
        LoadImage();
    }

    private void LoadText()
    {
        if (_text != null) return;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning("LoadText", gameObject);
    }

    private void LoadImage()
    {
        if (_image != null) return;
        _image = transform.Find("Image").GetComponent<Image>();
        Debug.LogWarning("LoadImage", gameObject);
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

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    protected override void OnClick()
    {
        Debug.Log("Item Clicked");
    }
}
