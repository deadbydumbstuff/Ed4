using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// A container has a set amount of itemSlots and cannot contain more then the specficies amount
/// it is also a singe page inventory with its owner being its typing
/// </summary>
public class Inventory_Container : MonoBehaviour,InventoryIf
{
    //TODO
    //controll the amount of items 
    //allow the player to move and pick up items from them

    public List<InventoryIf.Item> Inventory;
    Inventory_Manager inventory_manager;

    void start()
    {
        inventory_manager = GameObject.FindWithTag("GlobalManager").GetComponent<Inventory_Manager>();
        //get the inventory mananger
    }
}
