using UnityEngine;
using static InventoryIf;

public class Temp_OpenShop : MonoBehaviour,Interactable,InventoryIf
{
    public Npc_Core core;

    public InventoryIf.Inventory Inventory;
    MaterialPropertyBlock MatProBlk;

    void Start()
    {
        MatProBlk = new();
        GetComponent<SpriteRenderer>().GetPropertyBlock(MatProBlk);
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        MakeMPB();
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


    public void EnterRange()
    {
        MatProBlk.SetFloat("toggle", 1);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void ExitRange()
    {
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
        Inventory_Manager.IMinstace.CloseInventory(core.Inventory.invName);
    
}

    public void Interact()
    {
        Inventory_Manager.IMinstace.OpenInventory(1, core.Inventory, this.gameObject);
    }

    public InventoryIf.Inventory returnOwner()
    {
        return core.Inventory;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
