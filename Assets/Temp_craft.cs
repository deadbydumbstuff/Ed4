using UnityEngine;

public class Temp_craft : MonoBehaviour,InventoryIf
{
    public Inventory_ItemSlot slot1;//fox
    public Inventory_ItemSlot slot2; //snap
    public Inventory_ItemSlot output;

    public Temp_AskPlayerForHelp ask;
    public InventoryIf.Item RewardItem;
    public InventoryIf.Inventory RewardInventoy;

    bool close;
    public Temp_Drop end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (close)
        {
            if (RewardInventoy.Items.Count != 1)
            {
                Inventory_Manager.IMinstace.CloseInventoryINT(3);
            }
        }
    }


    public void onbutton()
    {
        if ( slot1.item == null || slot2.item == null) { return; }

        if (slot1.specItem == slot1.item.ItemType)
        {
            if (slot2.specItem == slot2.item.ItemType && slot1.item.Quantity >= 3 && slot2.item.Quantity >= 1)
            {
                compleated();
            }
        }
        else if (slot1.specItem == slot2.item.ItemType)
        {
            if (slot2.specItem == slot1.item.ItemType && slot2.item.Quantity >= 3 && slot1.item.Quantity >= 1)
            {
                compleated();
            }
        }
    }

    void compleated()
    {
        InventoryIf.Inventory ii = slot1.IPM.InvSource.GetComponent<InventoryIf>().returnOwner();
        InventoryIf.Item i1 = new InventoryIf.Item { ItemType = slot1.specItem, Quantity = 1 };
        InventoryIf.Item i2 = new InventoryIf.Item { ItemType = slot2.specItem, Quantity = 3 };
        ii.Items.Remove(i1);
        ii.Items.Remove(i2);



        Inventory_Manager.IMinstace.CloseInventoryINT(2);
        RewardInventoy.Items.Add(RewardItem);
        Inventory_Manager.IMinstace.OpenInventory(3,RewardInventoy,this.gameObject);
        close = true;

        // TimeBasedEvent tbe = this;
        if (end == null)
        {
            GameObject.FindFirstObjectByType<Temp_Drop>().Drop = true;
        }
        else
        {
            end.Drop = true;
        }
       // Time_Mananger.instance.CreateNewEvent(tbe, Time_Mananger.instance.CurrentDate, Time_Mananger.instance.Hour + 1, 0);
    }

    public InventoryIf.Inventory returnOwner()
    {
        return RewardInventoy;
    }

}
