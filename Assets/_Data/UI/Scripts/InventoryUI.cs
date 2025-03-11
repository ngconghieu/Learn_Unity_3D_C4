using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] protected BtnItem btnItem;
    [SerializeField] private bool isInventoryVisible = false;
    [SerializeField] private List<Item> items = new();
    private ListOfItems _listOfItems;

    private void Start()
    {
        Hide();
        HideDefaultItem();
        LoadParameters();
    }

    private void FixedUpdate()
    {
        //UpdateItems();
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

    private void LoadParameters()
    {
        _listOfItems = InventoryManager.Instance.GetInventory<ListOfItems>();
        items = _listOfItems.GetItems();
        AddItemsIntoInventory(items);
    }

    private void AddItemsIntoInventory(List<Item> items)
    {
        foreach (Item item in items)
        {
            BtnItem newBtnItem = Instantiate(btnItem, btnItem.transform.parent);
            newBtnItem.gameObject.SetActive(true);
        }
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
