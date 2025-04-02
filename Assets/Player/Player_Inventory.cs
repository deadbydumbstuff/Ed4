using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player_Inventory : MonoBehaviour,InventoryIf
{
    [Header("Debug")]
    [SerializeField]KeyCode SpawnItem;
    [SerializeField]ItemSObj DebugItem;

    [Header("Core")]

    public List<InventoryIf.Item> inventory = new();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SpawnItem))
        {
            //open da inventory
            //search inventory if contains this item allready if so quanity + 1
            inventory_Manager.AddItem(inventory, DebugItem, 1 );
            inventory_Manager.GeneratePage(inventory);
        }

        if (Input.GetKeyDown(inventoryKey))
        {
            //open da inventory
            PrintInventory();
        }
    }
    /// <summary>
    /// debugs out an entier inventory
    /// </summary>
    void PrintInventory()
    {
        foreach (InventoryIf.Item item in inventory)
        {
            Debug.Log(item.ItemType);
            Debug.Log(item.Quantity);
        }
    }
}
