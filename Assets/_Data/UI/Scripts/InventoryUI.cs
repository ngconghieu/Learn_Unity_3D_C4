using System;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] protected BtnItem btnItem;
    [SerializeField] private bool isInventoryVisible = false;
    private void Start()
    {
        Hide();
        HideDefaultItem();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnItem();
    }

    private void LoadBtnItem()
    {
        if (btnItem != null) return;
        btnItem = GetComponentInChildren<BtnItem>();
        Debug.LogWarning("LoadBtnItem", gameObject);
    }

    private void HideDefaultItem()
    {
        btnItem.gameObject.SetActive(false);
    }

    public void Hide()
    {
        isInventoryVisible = false;
        gameObject.SetActive(isInventoryVisible);
    }

    public void Show()
    {
        isInventoryVisible = true;
        gameObject.SetActive(isInventoryVisible);
    }

    public void Toggle()
    {
        if (isInventoryVisible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
