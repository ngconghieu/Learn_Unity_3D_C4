using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : Spawner<SpawnItem>
{
    [SerializeField] protected List<ItemCtrl> items = new();
    private ItemCtrl itemCtrl;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        //LoadBtnItem();
    }

    #region Load components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemCtrl();
    }

    private void LoadItemCtrl()
    {
        if(itemCtrl != null) return;
        itemCtrl = GetComponentInChildren<ItemCtrl>();
        itemCtrl.gameObject.SetActive(false);
        Debug.LogWarning("LoadItemCtrl", gameObject);
    }
    #endregion

    private void LoadBtnItem()
    {
        List<ItemProfiles> itemProfilesOfCash = InventoryManager.Instance.GetInventory<Cash>().GetItemProfiles();
        if(itemProfilesOfCash == null) return;
        foreach (var itemProfiles in itemProfilesOfCash)
        {
            ItemCtrl newItem = Instantiate(itemCtrl, transform.parent);
        }
    }

    public virtual void SpawnItems()
    {
        foreach (ItemCtrl item in items)
        {
            
        }
    }
}
