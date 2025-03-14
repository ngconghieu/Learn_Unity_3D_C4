using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DespawnItem : Despawner<ItemCtrl>
{
    [SerializeField] private ItemCtrl item;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private SphereCollider _collider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItem();
        LoadCollider();
    }

    private void LoadCollider()
    {
        if (_collider != null) return;
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = radius;
        Debug.LogWarning("LoadCollider", gameObject);
    }

    private void LoadItem()
    {
        if (item != null) return;
        item = GetComponentInParent<ItemCtrl>();
        Debug.LogWarning("LoadItem", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HeroCtrl>(out _))
        {
            HandleDespawnItem();
        }
    }

    private void HandleDespawnItem()
    {
        Despawn(item);
        ItemName test = (ItemName)Enum.Parse(typeof(ItemName), transform.parent.name);

        ItemProfiles itemProfiles = InventoryManager.Instance.GetInventory<Cash>().GetItemProfileByItemName(test);
        if (itemProfiles != null)
        {
            InventoryManager.Instance.GetInventory<Cash>().AddItem(test, 1);
            return;
        }
        InventoryManager.Instance.GetInventory<ListOfItems>().AddItem(test, 1);

    }
}
