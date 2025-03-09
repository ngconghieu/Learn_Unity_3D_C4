using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Cash : InventoryCtrl
{
    protected override void LoadItemProfiles()
    {
        Addressables.LoadAssetsAsync<ItemProfiles>(label, (ItemProfiles profiles) =>
        {
            itemProfiles.Add(profiles);
        });
    }
}
