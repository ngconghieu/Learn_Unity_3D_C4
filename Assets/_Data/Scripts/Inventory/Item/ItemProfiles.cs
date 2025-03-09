using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfiles", menuName = "SO/ItemProfiles")]
public class ItemProfiles : ScriptableObject
{
    public InventoryName InventoryName;
    public ItemName ItemName;
    public int maxStack;
}