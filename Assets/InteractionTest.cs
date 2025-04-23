using UnityEngine;
using System.Collections.Generic;
public class InteractionTest : MonoBehaviour,Interactable
{
    Inventory_Manager inventory;
    public List<InventoryIf.Item> InventoryTemp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<Inventory_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//when the plaer is interacted 
    {
        //just do whatever i want when interacted
        throw new System.NotImplementedException();
    }

    public void EnterRange() // when the player is close enough to the object //could make this a basic function and polumorhisisit
    {
        //create a popup to show thi ssitem is interactable
        //edit the sprit to have an outline? shaders?????
        //track 
        inventory.OpenInventory(1, "Chest", InventoryTemp);
        Debug.Log("test");
    }

    public void ExitRange()
    {
        inventory.CloseInventory("Chest");
        //throw new System.NotImplementedException();
    }
}
