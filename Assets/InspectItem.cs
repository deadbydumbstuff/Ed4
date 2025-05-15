using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class InspectItem : MonoBehaviour
{
    public Transform LAncore;
    public Transform RAncore;

    [SerializeField] GameObject Swap;
    [SerializeField] GameObject Drop;
    [SerializeField] GameObject SliderGobj;
    [SerializeField] GameObject Buy;
    [SerializeField] GameObject Sell;

    [SerializeField] TMP_Text textbox;
    [SerializeField] Slider Slider;

    private Inventory_Manager IM;
    [SerializeField] private InventoryIf.Item inspecteditem;
    private Inventory_Page_Manager inspectPage;
    //[SerializeField] InventoryIf.Item selectedItem;
    //THIS SCIPT change the active options on the inspection menu each of the buttons
    //depending on the type of inventory add diffrent options 

    //needed option
    // if the player owen the inventory
    //          Drop Item
    //          drop with scale
    //          if the other inventory is buying and selling
    //                     sell item
    private void Start()
    {
        IM = GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<Inventory_Manager>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param> the postion of the item slot to check if the inventory need to open on the left or right side of the screen
    public void Open(Vector3 pos) 
    {
        gameObject.SetActive(true);
        if (pos.x < 500)
        {
            //open via right ancor
            transform.position = pos - (LAncore.position - transform.position);
        }
        else
        {
            //open via left ancore
            transform.position = pos - (RAncore.position - transform.position);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Own"></param> i own this inventiry
    /// <param name="Single"></param> this is the only inventory open
    /// <param name="OwnOther"></param> i own the other inventory only matters if single = false
    /// <param name="Space"></param> the other inventory has space for another item
    public void Toggles(bool Own, bool Single, bool OwnOther, bool Space,bool Trade,bool TradableItem)
    {
        // opiko'l#;; toggle on and off the spesific settings for each menu 
        //penis!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Drop.gameObject.SetActive(false);
        Buy.gameObject.SetActive(false);
        Swap.gameObject.SetActive(false);
        Sell.gameObject.SetActive(false);
        SliderGobj.gameObject.SetActive(false);

        if (Own) {
            //drop
            Drop.gameObject.SetActive(true);
        }
        else if(!Trade && TradableItem) //dont own can trade
        {
            Buy.gameObject.SetActive(true);
            SliderGobj.gameObject.SetActive(true);
        }

        if (Single) //mutliple inventorys 
        {
            if (OwnOther) //
            {
                if (Own)
                {

                    Swap.gameObject.SetActive(true);
                }
            }
            else 
            { //dont own
                if (Space && Trade && TradableItem)
                {
                    SliderGobj.gameObject.SetActive(true);
                    Sell.gameObject.SetActive(true);
                }
                
            }
        }
    }

    public void UpdateStuff(InventoryIf.Item item,Inventory_Page_Manager Ipm)
    {
        textbox.text = item.ItemType.itemFlavourText;
        inspecteditem = item;
        inspectPage = Ipm;
    }
    public void SetBuySellQuantitiys(uint Quantity)
    {
        //slider set vaule to 0
        //set max to 
        Slider.value = 0;
        Slider.maxValue = Quantity;
    }

    public void UpdateSliderVaules()
    {
        //update the tex boxed on the slider to show the amount of selected item
        Buy.transform.GetChild(0).GetComponent<TMP_Text>().text = ($"Buy -- {Slider.value} : Cost");
        Sell.transform.GetChild(0).GetComponent<TMP_Text>().text = ($"Sell -- {Slider.value} : Quantity");
    }


    public void UpdateOnClick()
    {
        //when the buy or sell button is clicked update the items to the new vaules and stuff
        Slider.maxValue = Slider.maxValue - Slider.value;
    }


    public void DropButton()
    {
        //GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<Inventory_Manager>().Drop()
        Debug.Log("Click");
        IM.Drop(inspectPage.InvSource.GetComponent<InventoryIf>().returnOwner(),inspecteditem ,new Vector2 (0, 0));;
    }
    void SellButton()
    {

    }
    void BuyButton()
    {

    }
}
