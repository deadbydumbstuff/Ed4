using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public Transform LAncore;
    public Transform RAncore;

    [SerializeField] GameObject Swap;
    [SerializeField] GameObject Drop;
    [SerializeField] GameObject Buy;
    [SerializeField] GameObject Sell;
    //THIS SCIPT change the active options on the inspection menu each of the buttons
    //depending on the type of inventory add diffrent options 

    //needed option
    // if the player owen the inventory
    //          Drop Item
    //          drop with scale
    //          if the other inventory is buying and selling
    //                     sell item

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
    public void Toggles(bool Own, bool Single, bool OwnOther, bool Space,bool Trade)
    {
        // opiko'l#;; toggle on and off the spesific settings for each menu 
        //penis!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Own) {
            //drop
            Drop.gameObject.SetActive(true);
        }
        else if(Trade) //dont own can trade
        {
            Buy.gameObject.SetActive(true);
        }

        if (!Single) //mutliple inventorys 
        {
            if (OwnOther) //
            {
                if (Space)
                {
                    Swap.gameObject.SetActive(true);
                }
            }
            else 
            { //dont own
                if (Space && Trade)
                {
                    Sell.gameObject.SetActive(true);
                }
                
            }
        }
    }


}
