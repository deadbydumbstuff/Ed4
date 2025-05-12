using UnityEngine;

public class Item_Entity : MonoBehaviour, Interactable
{
    public InventoryIf.Item item;

    public void EnterRange() //toggle on
    {
        throw new System.NotImplementedException();
    }

    public void ExitRange() //toggleoff
    {
        throw new System.NotImplementedException();
    }

    public void Interact() // pick up
    {
        //give the player the item
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// creating an instance of an item entity with a item type and a quanitity
    /// </summary>
    public void SetItem(InventoryIf.Item Item)
    {
        GetComponent<SpriteRenderer>().sprite = Item.ItemType.itemIcon;
        item = Item;
    }

}
