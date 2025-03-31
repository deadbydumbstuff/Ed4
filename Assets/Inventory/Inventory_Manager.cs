using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public interface InventoryIf
{
    public class Item
    {
        public ItemSObj ItemType;
        public uint Quantity;
    }

}


public class Inventory_Manager : MonoBehaviour,InventoryIf,ItemInterface
{
    // this script will be a general manager for the inventory

    /// <summary>
    /// take a entier Inventory and returns only the items in a specifit typing
    /// </summary>
    /// <param name="Inventory"></param>
    /// <returns></returns>
    public List<InventoryIf.Item> OrganiseInventory(List<InventoryIf.Item> Inventory , ItemInterface.ItemType Type)
    {
        List < InventoryIf.Item > returnList = new();
        foreach(InventoryIf.Item Item in Inventory)
        {
            if (Item.ItemType.type == Type)
            {
                returnList.Add(Item);
            }
        }
        return returnList;
    }
    /// <summary>
    /// adds an item to an inventory with a quantitys while also creating a new itemspace if its a new item
    /// </summary>
    public void AddItem(List<InventoryIf.Item> Inventory, ItemSObj Item,uint Quantity)
    {
        //check the inventory for item
        foreach (InventoryIf.Item item in Inventory)
        {
            if (item.ItemType == Item)
            {
                item.Quantity += Quantity;
                return;
            }
        }
        Inventory.Add(new InventoryIf.Item { ItemType = Item,Quantity = Quantity});
    }

    public void RemoveItem(List<InventoryIf.Item> Inventory, ItemSObj Item, uint Quantity)
    {
        foreach (InventoryIf.Item item in Inventory)
        {
            if (item.ItemType == Item)
            {
                item.Quantity -= Quantity;
                return;
            }
        }
        //if the item isnt their then dont remove anything
    }

    /// <summary>
    /// returns true or false if an inventory contains a specific item type
    /// </summary>
    /// <param name="Inventory"></param> the inventory you check
    /// <param name="Item"></param> the item you look for
    /// <returns></returns>
    public bool InvContains(List<InventoryIf.Item> Inventory, ItemSObj Item)
    {
        foreach (var item in Inventory)
        {
            if (item.ItemType == Item)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// change the active page to new display
    /// </summary>
    /// <param name="Page"></param>
    public void OpenInventoryPage(GameObject Page)
    {
        Debug.Log($"Open {Page}");
    }

    /// <summary>
    /// when all inventory slots in all open pages are filled create a new one
    /// </summary>
    public void CreateNewPage(GameObject Page)
    {
        Debug.Log($"Clear {Page}");
    }

    /// <summary>
    /// turn the list of objects in the players inventory into displayed items on a a page
    /// </summary>
    void GeneratePage(List<InventoryIf.Item> Items)
    {

        ///foreach loop every item in an ineventory in the range of items in the list eg page one is 1-12 and 12-24 then page two is 24-36 and so on and displayu them in each open slot
    }
    /// <summary>
    /// remove all displayed items on every page or selected page
    /// </summary>
    void ClearPage()
    {
        //remove the renderd page
    }

    void ToolTip(InventoryIf.Item item)
    {
        // need to add a scaling factor so i can have the size of the text box scale with text density
        Debug.Log($"{item.ItemType.name} \n {item.ItemType.itemDescription}");
        // if itemfalour text != null 
        Debug.Log(item.ItemType.itemFlavourText); //fancy text bellow the original description
    }

    #region Trading
    //this will be for all the traiding functions
    /// <summary>
    /// take 2 inventorys exchange items, (inventory, item), (inventory,item)
    /// </summary>
    void ExampleTradeFunc()
    {
        //get the 2 player ineventory create a 
    }

    //inventory gives item gets gold , gets item gives gold
    void Sell()
    {

    }
    //i1 loses gold gets item, i2 loses item gets gold 
    void Buy()
    {

    }
    #endregion

}
