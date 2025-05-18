using UnityEngine;
using static InventoryIf;

public class Item_Entity : MonoBehaviour, Interactable
{
    public InventoryIf.Item item;
    MaterialPropertyBlock MatProBlk;

    private void Start()
    {
        MatProBlk = new MaterialPropertyBlock();
        GetComponent<SpriteRenderer>().GetPropertyBlock(MatProBlk);
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        MakeMPB();
    }
    public void MakeMPB()
    {
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }
    public void EnterRange()
    {
        MatProBlk.SetFloat("toggle", 1);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void ExitRange()
    {
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void Interact() // pick up
    {
        //give the player the item
        Debug.Log("Pickup");
        GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<Inventory_Manager>().AddItem(GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>().inventory, item.ItemType, item.Quantity);
        Destroy(this.gameObject);
        //destroy the interactable
    //    throw new System.NotImplementedException();
    }
    /// <summary>
    /// creating an instance of an item entity with a item type and a quanitity
    /// </summary>
    public void SetItem(InventoryIf.Item Item)
    {
        GetComponent<SpriteRenderer>().sprite = Item.ItemType.itemIcon;
        //item = Item;
        //item.Quantity = Item.Quantity;
    }

}
