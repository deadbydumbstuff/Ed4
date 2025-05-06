using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public interface InventoryIf
{
    [System.Serializable]
    public class Item
    {
        public ItemSObj ItemType;
        public uint Quantity;
    }

}
public interface OnClick
{
    public Inventory_ItemSlot OnItemClick();
}



public class Inventory_Manager : MonoBehaviour,InventoryIf,ItemInterface
{
    //meowmeow hold item
    public Inventory_ItemSlot selectedItem;//move this to the inventory selected
    //original ItemPos/slot/inventory?????????

    public GameObject ToolTip;
    [SerializeField] TMP_Text ToolText;//could move this to the tooltip function instead might be redunended
    [SerializeField] GameObject InspectMenu;
    public List<Inventory_Page_Manager> ItemPage;

    public GameObject itemSlotPrefab;
    void Update()
    {
        //on mouse click
        if (Input.GetMouseButtonDown(0)) { ClickCheck(); };

     
    }
    void ClickCheck()
    {
        //code from  https://www.youtube.com/watch?v=ptmum1FXiLE modified slightly in the for loop for interface usages
        PointerEventData PED = new PointerEventData(EventSystem.current);
        PED.position = Input.mousePosition;
        List<RaycastResult> RR = new List<RaycastResult>();
        EventSystem.current.RaycastAll(PED, RR);

        for (int i = 0; i < RR.Count; i++)
        {
            if (RR[i].gameObject.GetComponent<OnClick>() != null)
            {
                if (selectedItem != null)
                {
                    selectedItem.Deselected();
                }
                selectedItem = RR[i].gameObject.GetComponent<OnClick>().OnItemClick();
                //start the hold timer if the mouse is still down after the timer its a hold else do click stuff
                //start a timer corotine and if the mouse is still helder after this timere its a hold
                return;
            }
        }
        HideToolTip();
        if (selectedItem != null)
        {
            selectedItem.Deselected();
            selectedItem = null;
        }
    }

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
    public void AddItem(List<InventoryIf.Item> Inventory, ItemSObj Item,uint Quantity,string Owner)
    {
        //check the inventory for item
        foreach (InventoryIf.Item item in Inventory)
        {
            if (item.ItemType == Item)
            {
                item.Quantity += Quantity;
                UpdateInventory(Inventory, Item, Owner);
                return;
            }
        }
        Inventory.Add(new InventoryIf.Item { ItemType = Item,Quantity = Quantity});
        UpdateInventory(Inventory, Item, Owner);
    }

    public void RemoveItem(List<InventoryIf.Item> Inventory, ItemSObj Item, uint Quantity,string Owner)
    {
        foreach (InventoryIf.Item item in Inventory)
        {
            if (item.ItemType == Item)
            {
                item.Quantity -= Quantity;
                if (item.Quantity <= 0)
                {
                    foreach (Inventory_Page_Manager Page in ItemPage)
                    {
                        if (Page.pageOwner == Owner)
                        {
                            int i = Inventory.FindIndex(e => e.ItemType == Item);
                            Page.itemSlots.transform.GetChild(i).GetComponent<Inventory_ItemSlot>().ClearSlot();
                        }
                    }
                    Inventory.Remove(item);
                }
                else {
                    UpdateInventory(Inventory, Item, Owner);
                }
                break;      
            }
        }
    }

    public void UpdateInventory(List<InventoryIf.Item> Inventory, ItemSObj Item,string Owner)
    {
        //find item in inventory and the page
        foreach(Inventory_Page_Manager Page in ItemPage)
        {
            if (Page.pageOwner == Owner && Page.InventoryOpen) //add  check if inventory bool is open 
            {
                //this mean their is a open inventory displaying this charcter inventory
                int i= Inventory.FindIndex(e => e.ItemType == Item);
                //Inventory[i]
                if (Page.itemSlots.transform.childCount < Inventory.Count) //tomany items
                {
                    //if (!Page.fixedSlotCount) // page can have more items
                    //{
                        GameObject temp = Instantiate(itemSlotPrefab);
                        temp.transform.SetParent(Page.itemSlots.transform);
                        Page.RescaleItemZone();
                    //}
                    //else // page cant have more item so either return to original inventory or drop on the ground
                    //{
                    
                        //dropitem(item,quantity) create a item on the ground and stuff :3
                        //return
                    //}
                }
                Page.itemSlots.transform.GetChild(i).GetComponent<Inventory_ItemSlot>().SetItem(Inventory[i]);
                //go to page with that index vaule and update it with the new vaules
            }
        }
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
    /// turn the list of objects in the players inventory into displayed items on a a page
    /// </summary>
    /// could incule a page owener mechanic :3 
    public void GeneratePage(string inventoryOwner,List<InventoryIf.Item> Items,Inventory_Page_Manager IPM)
    {
        int i = 0;
        IPM.UpdatePage(inventoryOwner);
        IPM.RescaleItemZone();
        foreach (InventoryIf.Item ItemSlot in Items)
        {
            Transform Child = IPM.itemSlots.transform.GetChild(i);
            if (Child != null)
            {
                Inventory_ItemSlot Is = Child.GetComponent<Inventory_ItemSlot>();
                Is.SetItem(ItemSlot);
            }
            else
            {
                Debug.Log(" NO SLOT");
                //create a new item slot
                if (IPM.fixedSlotCount != true)
                {
                    Instantiate(itemSlotPrefab);
                }
                else
                {
                    //return the item to the floor or something
                }
                //set
            }
            i++;
        }
    }

    /// <summary>
    /// remove all displayed items on every page or selected page  ?why
    /// </summary>
    void ClearPage()
    {
        //remove the renderd page
    }

    public void RenderToolTip(InventoryIf.Item item,Vector3 pos)
    {
        // need to add a scaling factor so i can have the size of the text box scale with text densit
        // if itemfalour text != null 
        //Debug.Log(item.ItemType.itemFlavourText); //fancy text bellow the original description
        ToolTip.SetActive(true);
        ToolTip.transform.position = pos;
        ToolText.text = ($"{item.ItemType.itemName} \n {item.ItemType.itemDescription}");
    }
    public void HideToolTip()
    {
        ToolTip.SetActive(false);
    }
    /// <summary>
    ///  
    /// </summary>
    /// <param name="item"></param> the item selected
    /// //the inventory it belongs 2
    /// deciper the state/type of the inventory 
    public void InspectItem(InventoryIf.Item item, Inventory_Page_Manager iPM,string PageOwner)
    {
        //get the postion of the item slo


        //check the other pages
        foreach (Inventory_Page_Manager ipm in ItemPage)
        {
            if (ipm == iPM) { break; }
            // check the state of this inventory and if open
            switch (ipm.inventoryType)
            {
                case Inventory_Page_Manager.InventoryType.PlayerInventory:
                    //
                    break;
                case Inventory_Page_Manager.InventoryType.Trade:
                    //
                    break;
            }


        }

    }

    public void OpenInventory(int Page, string inventoryOwner, List<InventoryIf.Item> Items)
    {
        //selected the page 
        //get the IMP FROM THE page use that for the gen page scriot
        //active the page
        //ItemPage[Page].itemSlots
        
        Inventory_Page_Manager IPM = ItemPage[Page];
        if (IPM.pageOwner == inventoryOwner && IPM.InventoryOpen == true)
        {
            CloseInventory(inventoryOwner);
            return;
        }
        IPM.InventoryOpen = true;
        IPM.gameObject.SetActive(true);
        GeneratePage(inventoryOwner, Items, IPM);
        //open the gameobjects/setactive
        //generatepage
    }
    public void CloseInventory(string inventoryOwner)
    {
        foreach (Inventory_Page_Manager IPM in ItemPage)
        {
            if (IPM.pageOwner == inventoryOwner)
            {
                IPM.InventoryOpen = false;
                IPM.gameObject.SetActive(false);
            }
        }
        ToolTip.SetActive(false);
    }
    public void CloseInventoryINT(int Page)
    {
        ItemPage[Page].InventoryOpen= false;
        ItemPage[Page].gameObject.SetActive(false);    
        ToolTip.SetActive(false);
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
