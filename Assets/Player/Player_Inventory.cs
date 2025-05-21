using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player_Inventory : MonoBehaviour, InventoryIf
{
    [Header("Debug")]
    public static Player_Inventory instance;
    [SerializeField] bool DebugOn;
    [SerializeField]KeyCode SpawnItem;
    [SerializeField]ItemSObj DebugItem;
    [SerializeField]ItemSObj DebugAntherItem;

    [Header("Core")]
    public Player_Core Core;
    public InventoryIf.Inventory inventory = new();
    public KeyCode inventoryKey;
    [SerializeField] Inventory_Manager inventory_Manager;

    [Space][Header("Ui")]
    public GameObject Ui;
    //get the other ui
    //pages
    //inventoryslots
    //displays

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        inventory_Manager.GeneratePage(inventory, inventory_Manager.ItemPage[0],this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugOn && Input.GetKeyDown(SpawnItem))
        {
            //open da inventory
            //search inventory if contains this item allready if so quanity + 1
            inventory_Manager.AddItem(inventory, DebugItem, 1);
            
        }

        if (Input.GetKeyDown(inventoryKey))
        {
            //open da inventory
            OpenInventory();
        }
    }

    #region Debug_Funcs
    public void Debug_AddItem()
    {
        inventory_Manager.AddItem(inventory, DebugAntherItem, 1);
    }
    public void Debug_RemoveItem()
    {
        if (inventory.Items.Count <= 0) { Debug.Log("no items"); return; }
        inventory_Manager.RemoveItem(inventory, inventory.Items.Last().ItemType, 1);
    }
    /// <summary>
    /// debugs out an entier inventory
    /// </summary>
    void OpenInventory()
    {
        //open inventoryu function pass in the relevent information and displat
        //the set invenotry two true 
        // if inventory open then close it instead :3 (disable inventory and change bool and set type)
        inventory_Manager.OpenInventory(0, inventory,this.gameObject);
    }

    public InventoryIf.Inventory returnOwner()
    {
        return inventory;
    }
    #endregion
}
