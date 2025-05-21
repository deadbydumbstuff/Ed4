using UnityEngine;

public class Temp_HelpEvent : MonoBehaviour, OnDiaougeEnd
{
    public Temp_Dialouge.Text Text;


    //player leabes house
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDiaougeEnd e = this;
        Temp_Dialouge.instance.NewDialouge(Text, e);
    }
    //starts conversation with npc
    //conversation functions
    //once dialouge ends
        //NPC Pathfind to location
    public void end()
    {
        throw new System.NotImplementedException();
    }

    //another npc waits and on interaction open a menu showing 2 items slots waitinf ro 2 items

    // in the nearbuy area are 2 herdbal items that can be placed into the invetorys
            //one is nearbu
            // the other is sold in a shop
    //once both items are in the inventorys button show up and be like CRAFT!

    //end tutorial
   
}
