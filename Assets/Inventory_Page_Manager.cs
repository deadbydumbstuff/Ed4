using UnityEngine;
using TMPro;
public class Inventory_Page_Manager : MonoBehaviour
{
    public string pageOwner;
    [SerializeField] TMP_Text ownerDisplay;
    public Transform itemSlots;
    //other ui
    public bool InventoryOpen;
    [SerializeField] ScaleWithGridLayout swgl;
    public InventoryType inventoryType;
    public enum InventoryType
    {
        PlayerInventory,//full controll of inventory, drop , swap , if other inventory is tradi 
        Trade,//npcs , buy sell
        Locked, // you can view just not buy or trade
    }

    public bool fixedSlotCount;

    public void UpdatePage(string OwnerName)
    {
        pageOwner = OwnerName;
        ownerDisplay.text = pageOwner;
        //scale with gridlayout function
    }
    

    public void RescaleItemZone()
    {
        swgl.updateScale();
    }
}
