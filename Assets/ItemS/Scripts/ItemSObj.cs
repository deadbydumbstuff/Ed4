using UnityEngine;

[CreateAssetMenu(fileName = "ItemSObj", menuName = "Scriptable Objects/ItemSObj")]
public class ItemSObj : ScriptableObject,ItemInterface
{
    public string itemName;
    public string itemDescription;
    public string itemFlavourText;//optional and will be displayed in a fnacy font and in ittatlics
    public int baseCost;
    public Sprite itemIcon;

    public ItemInterface.ItemType type;
    //think of a method to have items have custom effects
    //public string[] ItemCustomEffects;

    public bool ingredient;
    public bool equiable;
    public bool stackable;
    public bool notTradeable;
}
