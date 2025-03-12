using UnityEngine.AddressableAssets;

public class ListOfItems : InventoryCtrl
{
    protected override void LoadItemProfiles()
    {
        Addressables.LoadAssetsAsync<ItemProfiles>(label, (ItemProfiles profiles) =>
        {
            itemProfiles.Add(profiles);
        });
    }
}
