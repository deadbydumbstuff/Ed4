using UnityEngine;
using System.Collections.Generic;
public class InteractionTest : MonoBehaviour,Interactable,InventoryIf
{
    Inventory_Manager inventory;
    //public List<InventoryIf.Item> InventoryTemp;
    public InventoryIf.Inventory Inventory;
    MaterialPropertyBlock MatProBlk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<Inventory_Manager>();
        MatProBlk = new();
        GetComponent<SpriteRenderer>().GetPropertyBlock(MatProBlk);
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        MakeMPB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//when the plaer is interacted 
    {
        //just do whatever i want when interacted
        inventory.OpenInventory(1, Inventory,this.gameObject);
       // throw new System.NotImplementedException();
    }

    public void EnterRange() // when the player is close enough to the object //could make this a basic function and polumorhisisit
    {
        //create a popup to show thi ssitem is interactable
        //edit the sprit to have an outline? shaders?????
        //track 
        //inventory.OpenInventory(1, "Chest", InventoryTemp);
        MatProBlk.SetFloat("toggle", 1);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
        //Debug.Log("test");
    }

    public void ExitRange()
    {
        inventory.CloseInventory(Inventory.invName);
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
        //throw new System.NotImplementedException();
    }
   

    public void MakeMPB()
    {
        //apply the material property block
        //setfloat
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
        //
    }

    private void OnValidate()
    {
        MatProBlk = new();
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public InventoryIf.Inventory returnOwner()
    {
        return Inventory;
    }
}
