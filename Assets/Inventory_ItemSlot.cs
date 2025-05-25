using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Inventory_ItemSlot : MonoBehaviour,InventoryIf,OnClick
{
    public InventoryIf ItemRestricions; // only items of these types are allowed
    public Inventory_Manager Im;
    public Inventory_Page_Manager IPM;
    [Header("")]
    public InventoryIf.Item item;
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text TextBox;
    [SerializeField] Sprite Empty;

    public Color SelectedColour;

    public bool SpecifiedSlot;// if this is true this item slot will only accept one type of item
    public ItemSObj specItem; // the item type

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Im = GameObject.FindWithTag("GlobalManager").GetComponent<Inventory_Manager>();
        IPM = gameObject.transform.parent.GetComponent<ScaleWithGridLayout>().IPM;
    }

    public void SetItem(InventoryIf.Item Item)
    {
        ItemImage.sprite = Item.ItemType.itemIcon;
        TextBox.text = ($"{Item.Quantity}");
        item = Item;
    }
    //dosnt remove items jusst the slot currently being renders  :3
    public void ClearSlot()
    {

        item = null;
        ItemImage.sprite = Empty;
        TextBox.text = ($"");
    }

    public Inventory_ItemSlot OnItemClick()
    {
        //selected the item and set a bool in the inv manager 
        Im.HideToolTip();
        Im.InspectMenu.SetActive(false);
        GetComponent<Image>().color = SelectedColour;
        if (item != null && item.Quantity > 0)
        {
            StopCoroutine(ShowToolTip());
            Im.InspectItem(item, IPM, transform.position);
        }
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
        if (item != null && item.ItemType != null) { StartCoroutine(ShowToolTip()); }
    }

    IEnumerator ShowToolTip()
    {
        //yeild wait time till show coroutine
        yield return new WaitForSeconds(0.4f);
        if (item != null && item.ItemType != null) { Im.RenderToolTip(item, transform.position); }
    }

    public InventoryIf.Inventory returnOwner()
    {
        return null;
    }
}
