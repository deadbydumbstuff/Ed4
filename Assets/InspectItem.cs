using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public Transform LAncore;
    public Transform RAncore;
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
        Debug.Log(pos);
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

    public void Toggles()
    {
        // opiko'l#;; toggle on and off the spesific settings for each menu 
        //penis!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }


}
