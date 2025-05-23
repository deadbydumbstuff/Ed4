using UnityEngine;

public class Temp_HelpEvent : MonoBehaviour, OnDiaougeEnd
{
    //act one of this event
    public Temp_Dialouge.Text Text;
    public Temp_NpcPathFinding NPC;
    public bool EventDone;

    //player leabes house
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!EventDone)
        {
            EventDone = true;
            OnDiaougeEnd e = this;
            Temp_Dialouge.instance.NewDialouge(Text, e);
        }
    }
    //starts conversation with npc
    //conversation functions
    //once dialouge ends
        //NPC Pathfind to location
    public void end()
    {
        //start the npc pathfind
        //start pathfinding  if the player leaves a range stop moving :3

        //update locatio and lock movement untill then
        NPC.Path = Npc_MasterScript.instance.GeneratePath((Vector2)NPC.gameObject.transform.position, new(4, -21));
        NPC.goalPos = new(4, -21);
        NPC.PathFind = true;
       // throw new System.NotImplementedException();
    }

    //another npc waits and on interaction open a menu showing 2 items slots waitinf ro 2 items

    // in the nearbuy area are 2 herdbal items that can be placed into the invetorys
            //one is nearbu
            // the other is sold in a shop
    //once both items are in the inventorys button show up and be like CRAFT!

    //end tutorial
   
}
