using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;
using static Unity.Burst.Intrinsics.X86.Avx;

public interface InventoryIf
{
    [System.Serializable]
    public class Item
    {
        public ItemSObj ItemType;
        public uint Quantity;
    }
    [System.Serializable]
    public class Inventory
    {
        public string invName;
        public Inventory_Page_Manager.InventoryType InventoryType;
        public List<Item> Items;
    }

    public InventoryIf.Inventory returnOwner();
}

public interface OnClick
{
    public Inventory_ItemSlot OnItemClick();
}



public class Inventory_Manager : MonoBehaviour, InventoryIf, ItemInterface
{
    public static Inventory_Manager IMinstace;
    //meowmeow hold item
    public Inventory_ItemSlot selectedItem;//move this to the inventory selected
    //original ItemPos/slot/inventory?????????

    public GameObject ToolTip;
    [SerializeField] TMP_Text ToolText;//could move this to the tooltip function instead might be redunended
    public GameObject InspectMenu;
    public List<Inventory_Page_Manager> ItemPage;
    public Inventory_Page_Manager InspectedPage;
    public GameObject itemSlotPrefab;
    public GameObject Item_Entity;

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
        bool hit = false; ;
        for (int i = 0; i < RR.Count; i++)
        {
            if (RR[i].gameObject.GetComponent<OnClick>() != null)
            {
                hit = true;
                if (selectedItem != null)
                {
                    selectedItem.Deselected();
                    // InspectMenu.gameObject.SetActive(false);
                }
                selectedItem = RR[i].gameObject.GetComponent<OnClick>().OnItemClick();
                //start the hold timer if the mouse is still down after the timer its a hold else do click stuff
                //start a timer corotine and if the mouse is still helder after this timere its a hold
                return;
            }
        }
        if (!hit)
        {
            InspectMenu.gameObject.SetActive(false);
        }
        HideToolTip();
        // InspectMenu.gameObject.SetActive(false);
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
    public List<InventoryIf.Item> OrganiseInventory(InventoryIf.Inventory Inventory, ItemInterface.ItemType Type)
    {
        List<InventoryIf.Item> returnList = new();
        foreach (InventoryIf.Item Item in Inventory.Items)
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
    public void AddItem(InventoryIf.Inventory Inventory, ItemSObj Item, uint Quantity)
    {
        //check the inventory for item
        foreach (InventoryIf.Item item in Inventory.Items)
        {
            if (item.ItemType == Item)
            {
                item.Quantity += Quantity;
                UpdateInventory(Inventory, Item);
                return;
            }
        }
        Inventory.Items.Add(new InventoryIf.Item { ItemType = Item, Quantity = Quantity });
        UpdateInventory(Inventory, Item);
    }

    public void RemoveItem(InventoryIf.Inventory Inventory, ItemSObj Item, uint Quantity)
    {
        foreach (InventoryIf.Item item in Inventory.Items)
        {
            if (item.ItemType == Item)
            {
                item.Quantity -= Quantity;
                if (item.Quantity <= 0)
                {
                    List<Inventory_Page_Manager> Ipm = new();

                    foreach (Inventory_Page_Manager Page in ItemPage)
                    {
                        if (Page.pageOwner == Inventory.invName)
                        {
                            int i = Inventory.Items.FindIndex(e => e.ItemType == Item);
                            Page.itemSlots.transform.GetChild(i).GetComponent<Inventory_ItemSlot>().ClearSlot();
                            //GeneratePage(Inventory, Page, Page.InvSource);
                            Ipm.Add(Page);
                        }
                    }
                    Inventory.Items.Remove(item);
                    foreach (Inventory_Page_Manager ipm in Ipm)
                    {
                        GeneratePage(Inventory, ipm, ipm.InvSource);
                    }
                    // Inventory.Items.Remove(item);//refesh the rest of the inventory to update

                }
                else {
                    UpdateInventory(Inventory, Item);
                }
                break;
            }
        }
    }

    public void UpdateInventory(InventoryIf.Inventory Inventory, ItemSObj Item)
    {
        //find item in inventory and the page
        foreach (Inventory_Page_Manager Page in ItemPage)
        {
            if (Page.InventoryOpen && Page.pageOwner == Inventory.invName) //add  check if inventory bool is open 
            {
                //this mean their is a open inventory displaying this charcter inventory
                int i = Inventory.Items.FindIndex(e => e.ItemType == Item);
                //Inventory[i]
                if (Page.itemSlots.transform.childCount < Inventory.Items.Count) //tomany items
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
                Page.itemSlots.transform.GetChild(i).GetComponent<Inventory_ItemSlot>().SetItem(Inventory.Items[i]);
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
    public void GeneratePage(InventoryIf.Inventory Inventory, Inventory_Page_Manager IPM,GameObject Source)
     {
        IPM.InvSource = Source;

        //get sauce
        IPM.GoldDisplay.SetActive(true);
        if (Source.GetComponent<Player_Core>() != null)
        {
            IPM.GoldDisplay.transform.GetChild(0).GetComponent<TMP_Text>().text = ($"Gold : {Source.GetComponent<Player_Core>().Gold}");
        }
        else if (Source.GetComponent<Npc_Core>() != null)
        {
            IPM.GoldDisplay.transform.GetChild(0).GetComponent<TMP_Text>().text = ($"Gold : {Source.GetComponent<Npc_Core>().Gold}");
        }
        else
        {
            IPM.GoldDisplay.SetActive(false);
        }

        int i = 0;
        InspectMenu.SetActive(false);
        IPM.UpdatePage(Inventory.invName);
        IPM.RescaleItemZone();
        IPM.inventoryType = Inventory.InventoryType;
        foreach (InventoryIf.Item ItemSlot in Inventory.Items)
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
        //after each loop i goes up then use the reming item slots in the diplayed inventiry and clear them 
        for (int o = 0; o <= Inventory.Items.Count - i; o++)
        {
            IPM.itemSlots.transform.GetChild(i + o).GetComponent<Inventory_ItemSlot>().ClearSlot();

        }
    }

    /// <summary>
    /// remove all displayed items on every page or selected page  ?why
    /// </summary>
    void ClearPage()
    {
        //remove the renderd page
    }

    public void RenderToolTip(InventoryIf.Item item, Vector3 pos)
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
    public void InspectItem(InventoryIf.Item Inventory, Inventory_Page_Manager IMP, Vector2 ItemSlotPos)
    {
        ToolTip.SetActive(false);
        InspectedPage = IMP;
        bool inv1 = false;
        bool inv2 = false;
        bool singleInv = false;
        //get the postion of the item slo
        // the type of inventory interacting with
        switch (IMP.inventoryType)
        {
            case Inventory_Page_Manager.InventoryType.PlayerInventory:
                inv1 = true;
                break;
            case Inventory_Page_Manager.InventoryType.Trade:
                inv1 = false;
                break;
        }
        bool space = false;
        //the other open inventory
        foreach (Inventory_Page_Manager ipm in ItemPage)
        {
            bool stop = false;
            if ((ipm.InventoryOpen && ipm != IMP) && !stop)
            {
                // check the state of this inventory and if open
                singleInv = true;
                stop = true;
                //idk why i have this  but its to tell what type of inventory;page and what other pages are their i need to add options such as sell/trade/deposit/
                switch (ipm.inventoryType)
                {
                    case Inventory_Page_Manager.InventoryType.PlayerInventory:
                        inv2 = true;
                        // if i own the first one 2 its a swap
                        //trade
                        break;
                    case Inventory_Page_Manager.InventoryType.Trade:
                        inv2 = false;
                        //if i own the first one its a trade
                        //own
                        break;
                }
                //space
                //get the inventor that oage owns
                if (ipm.itemSlots.GetChild(ipm.itemSlots.childCount - 1).GetComponent<Inventory_ItemSlot>().item != null)
                {
                    space = true;
                }
            }

        }
        // Debug.Log(singleInv);

        //create the bools
        //bool trade;
        bool tradeable = !Inventory.ItemType.notTradeable;


        //depending on the posion of the itemslot/page open  the inspecion pannel on a diffrent side of the screen so its readable

        //toggle on and off options depending on the stuff
        //pass in the item and the pages that are active onto the 
        //caculate the sell prices of buiying
        //just toggle stuff move object 
        InspectMenu.GetComponent<InspectItem>().Open(ItemSlotPos);
        InspectMenu.GetComponent<InspectItem>().Toggles(inv1, singleInv, inv2, space, !inv1, tradeable);
        InspectMenu.GetComponent<InspectItem>().UpdateStuff(Inventory,InspectedPage);
        InspectMenu.GetComponent<InspectItem>().SetBuySellQuantitiys(Inventory.Quantity);
        //pass the item and invetorypmg
    }

    public void OpenInventory(int Page, InventoryIf.Inventory Inventory,GameObject Source)
    {
        //selected the page 
        //get the IMP FROM THE page use that for the gen page scriot
        //active the page
        //ItemPage[Page].itemSlots
        
        Inventory_Page_Manager IPM = ItemPage[Page];
        IPM.InvSource = Source;
        if (IPM.pageOwner == Inventory.invName && IPM.InventoryOpen == true)
        {
            CloseInventory(Inventory.invName);
            return;
        }
        IPM.InventoryOpen = true;
        IPM.gameObject.SetActive(true);
        GeneratePage(Inventory, IPM,Source);

        //updae the inspection pannel unless it override the current pannel
        if (InspectedPage != null && IPM == InspectedPage)
        {
            //overiding the current
            Debug.Log("ovverride");
            //hide
        }
        else
        {
            //update the invetory
            Debug.Log("underride");
        }
    }
    public void CloseInventory(string inventoryOwner)
    {
        foreach (Inventory_Page_Manager IPM in ItemPage)
        {
            if (IPM.pageOwner == inventoryOwner && IPM != null)
            {
                IPM.InventoryOpen = false;
                IPM.gameObject.SetActive(false);
            }
        }
        ToolTip.SetActive(false);
        // RefreshInspectMenu();

        if (InspectedPage != null && inventoryOwner == InspectedPage.pageOwner)
        {
            InspectMenu.gameObject.SetActive(false);
            Debug.Log("meow");
        }
        else
        {
            RefreshInspectMenu();
        }
    }
    public void CloseInventoryINT(int Page)
    {
        ItemPage[Page].InventoryOpen = false;
        ItemPage[Page].gameObject.SetActive(false);
        ToolTip.SetActive(false);
        //refresh.update thee inspection menu 
        if (InspectedPage != null && ItemPage[Page] != InspectedPage)
        {
            InspectedPage.gameObject.SetActive(false);
        }
        else
        {
            RefreshInspectMenu();
        }
    }

    void RefreshInspectMenu()
    {
        //check what pages are open and redo it
        Debug.Log("test");
        //if inventory is the ond the inspect pannle is on then close the inspec pann
    }

    #region TRADING
    public void Swap(InventoryIf.Inventory reciver, InventoryIf.Inventory giver, InventoryIf.Item item)
    {
        RemoveItem(giver, item.ItemType, item.Quantity);
        AddItem(reciver, item.ItemType, item.Quantity);
        //Debug.Log("wtf going on");
    }
    public void Drop(InventoryIf.Inventory Inventory, InventoryIf.Item item1,Vector2 dropPosition)
    {
        //create an item entity on the groyp with the type and quaantity
        GameObject item = Instantiate(Item_Entity);
        item.transform.position = dropPosition;
        Debug.Log(item1.Quantity);
        item.GetComponent<Item_Entity>().item = new InventoryIf.Item { ItemType = item1.ItemType,Quantity = item1.Quantity} ;
        item.GetComponent<Item_Entity>().SetItem(item1);
        //item.GetComponent<SpriteRenderer>().sprite = item1.ItemType.itemIcon;
        RemoveItem(Inventory, item1.ItemType, item1.Quantity);//remove the item from the inventory 
    }

    InventoryIf.Inventory InventoryIf.returnOwner()
    {
        return null;
    }
    /// <summary>
    /// givin a inventory it will return the first open inventory not the given one 
    /// </summary>
    /// <returns></returns>
    public Inventory_Page_Manager returnOtherOpenInventory(Inventory_Page_Manager Ipm)
    {
        foreach (Inventory_Page_Manager ipm in ItemPage)
        {
            if (ipm.InventoryOpen && ipm != Ipm)
            {
                return ipm;
            }
        }


        return null;
    }
    #endregion
    //send a single with a string and every invetory has the funcion 
    // return script or something with this inventory/return the invetory for the stuff

}
