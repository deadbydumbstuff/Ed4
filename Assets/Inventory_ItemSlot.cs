using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Inventory_ItemSlot : MonoBehaviour,InventoryIf
{
    public InventoryIf ItemRestricions; // only items of these types are allowed

    [Header("")]
    [SerializeField] UnityEngine.UI.Image ItemImage;
    [SerializeField] TMP_Text TextBox;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ClearSlot();
        }
    }

    public void SetItem(InventoryIf.Item Item)
    {
        //set the vaules from the scriptible object 
        //set itemframe image to item.tiemtype.sprite 
        //set text to item wquaNITY
        //OPTIONAL frame is based on item type
        ItemImage.sprite = Item.ItemType.itemIcon;
        TextBox.text = ($"{Item.Quantity}");
    }

    public void ClearSlot()
    {
        //remove the item stored in that slot
        //idk how to do this yet tho :£
        ItemImage.sprite = null;
        TextBox.text = ($"");
    }
}
