using UnityEngine;

public class Temp_AskPlayerForHelp : MonoBehaviour,Interactable,OnDiaougeEnd,InventoryIf
{
    public static Temp_AskPlayerForHelp m_AskPlayerForHelp;
    public Temp_Dialouge.Text Text;
    public Temp_Dialouge.Text Text2;//asking if you have found them
    //temptemp tempy;
    public InventoryIf.Inventory Inv;




    public InventoryIf.Inventory Inventory;
    MaterialPropertyBlock MatProBlk;

    void Start()
    {
        m_AskPlayerForHelp = this;
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




    public bool askingforhelp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //OnDiaougeEnd e = this;
        if (!askingforhelp)
        {
            Temp_Dialouge.instance.NewDialouge(Text, null);
            askingforhelp = true;
        }
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
        Inventory_Manager.IMinstace.CloseInventoryINT(2);
        Inventory_Manager.IMinstace.CloseInventoryINT(3);
        Temp_Dialouge.instance.CloseDialouge();
    }

    public void Interact()
    {
        if (askingforhelp)
            {
           // Debug.Log("Interact");
            OnDiaougeEnd e = this;
            Temp_Dialouge.instance.NewDialouge(Text2, e);
        }
        //open text then at the end of the text menu open a inventiro for 2 items to be placed
    }

    public void end()
    {
       // Debug.Log("OpenInv");
        //throw new System.NotImplementedException();
        Inventory_Manager.IMinstace.OpenInventory(2, Inv, this.gameObject);
    }


    public InventoryIf.Inventory returnOwner()
    {
        return Inv;
    }
}
