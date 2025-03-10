using System;
using UnityEngine;

public class InventoryUI : GameMonoBehaviour
{
    [SerializeField] private bool isInventoryVisible = false;
    private void Start()
    {
        HideInventory();
    }

    private void HideInventory()
    {
        isInventoryVisible = false;
        gameObject.SetActive(false);
    }

    private void ShowInventory()
    {
        isInventoryVisible = true;
        gameObject.SetActive(true);
    }
}
