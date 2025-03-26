using UnityEngine;
using System.Collections.Generic;

public class Player_Inventory : MonoBehaviour
{
    Dictionary<ItemSObj,int> inventory = new(); //item,quantity


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Canvas"></param> a prefab of the required inventory objected needed to render   ,and their item slots
    /// <param name="inventory"></param>
    public void RenderInventory(GameObject Canvas, Dictionary<ItemSObj, int> inventory)
    {
        //get the parent of all the inventory slots (could have thse sorted to pages aswell
        //for each every item slot
        //if more then limit then active the pages sysetet to flip through them
    }

    public void Trade(GameObject Npc, Dictionary<ItemSObj, int> inventory)
    {
        // 
    }

}
