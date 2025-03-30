using UnityEngine;

[CreateAssetMenu(fileName = "ItemSObj", menuName = "Scriptable Objects/ItemSObj")]
public class ItemSObj : ScriptableObject,ItemInterface
{
    public string ItemName;
    public int baseCost;
    public Sprite itemIcon;

    public ItemInterface.ItemType type;
    //think of a method to have items have custom effects
    //public string[] ItemCustomEffects;

    public bool ingredient;
    public bool equiable;
    public bool stackable;
}
