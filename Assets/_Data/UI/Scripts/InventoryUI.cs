using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] protected BtnItem btnItem;
    [SerializeField] private bool isInventoryVisible = false;
    [SerializeField] private Dictionary<Item, BtnItem> _btnItems = new();

    private ListOfItems _listOfItems;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        Hide();
        SetParameters();
    }

    private void OnEnable()
    {
        if (_listOfItems == null) return;
        _listOfItems.OnInventoryChanged += UpdateItems;
    } // Subscribe to event

    private void OnDisable()
    {
        if (_listOfItems == null) return;
        _listOfItems.OnInventoryChanged -= UpdateItems;
    } // Unsubscribe from event

    #region Load Components
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
    #endregion

    private void SetParameters()
    {
        btnItem.gameObject.SetActive(false); // Hide default item
        _listOfItems = InventoryManager.Instance.GetInventory<ListOfItems>();
        _listOfItems.OnInventoryChanged += UpdateItems;
    }

    private void UpdateItems()
    {
        List<Item> items = _listOfItems.GetItems();
        List<Item> itemsToRemove = new();
        foreach (Item item in items)
        {
            if (_btnItems.TryGetValue(item, out BtnItem btn)) //Check if item exists
            {
                if(item.amount == 0)
                {
                    itemsToRemove.Add(item);
                    continue;
                }
                if(btn.GetAmount() != item.amount) //Check if amount has changed
                    btn.SetAmount(item.amount);
                continue;
            }
            //Handle creation of new item
            BtnItem newBtnItem = Instantiate(btnItem, btnItem.transform.parent);
            newBtnItem.SetItem(item);
            newBtnItem.SetAmount(item.amount);
            newBtnItem.name = item.itemProfiles.ItemName.ToString() + "-" + item.itemID;
            _btnItems[item] = newBtnItem;
            newBtnItem.gameObject.SetActive(true);
        }
        if(itemsToRemove.Count == 0) return;
        foreach (Item item in itemsToRemove)
        {
            GameObject.Destroy(_btnItems[item].gameObject);
            _btnItems.Remove(item);
        }
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
        isInventoryVisible = !isInventoryVisible;
        gameObject.SetActive(isInventoryVisible);
    }

}
