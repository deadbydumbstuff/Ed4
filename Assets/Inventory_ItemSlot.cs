using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Inventory_ItemSlot : MonoBehaviour,InventoryIf,OnClick
{
    public InventoryIf ItemRestricions; // only items of these types are allowed
    public Inventory_Manager Im;
    [SerializeField] Inventory_Page_Manager IPM;
    [Header("")]
    public InventoryIf.Item item;
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text TextBox;
    [SerializeField] Sprite Empty;

    public Color SelectedColour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Im = GameObject.FindWithTag("GlobalManager").GetComponent<Inventory_Manager>();
        IPM = gameObject.transform.parent.GetComponent<ScaleWithGridLayout>().IPM;
    }

    public void SetItem(InventoryIf.Item Item)
    {
        //set the vaules from the scriptible object 
        //set itemframe image to item.tiemtype.sprite 
        //set text to item wquaNITY
        //OPTIONAL frame is based on item type
        ItemImage.sprite = Item.ItemType.itemIcon;
        TextBox.text = ($"{Item.Quantity}");
        item = Item;
    }
    //dosnt remove items jusst the slot currently being renders  :3
    public void ClearSlot()
    {
        //remove the item stored in that slot
        //idk how to do this yet tho :£
        ItemImage.sprite = Empty;
        TextBox.text = ($"");
    }

    public Inventory_ItemSlot OnItemClick()
    {
        //selected the item and set a bool in the inv manager 
        Im.HideToolTip();
        Im.InspectMenu.SetActive(false);
        GetComponent<Image>().color = SelectedColour;
        if (item.Quantity > 0)
        {
            StopCoroutine(ShowToolTip());
            Im.InspectItem(item, IPM, transform.position);
        }
        //show the item options
        //  drop/Buy
        //  open inspection menu and check what type of inventory it its rn // selling // chest // player inventory // what the other types of inventory are 
        //  split? create a new slot
        //  inspec? nah
        //  darken this item
        return this;
    }
    public void Deselected()
    {
        //reverse the events when a mouse click
        GetComponent<Image>().color = Color.white;
    }


    public void HideToolTipCall()
    {
        Im.HideToolTip();
        //cancel the ienumrator to
        StopCoroutine(ShowToolTip());
    }

    public void HoverOver()
    {
        if (item.ItemType != null) { StartCoroutine(ShowToolTip()); }
    }

    IEnumerator ShowToolTip()
    {
        //yeild wait time till show coroutine
        yield return new WaitForSeconds(0.4f);
        Im.RenderToolTip(item, transform.position);
    }

    public InventoryIf.Inventory returnOwner()
    {
        return null;
    }
}
