using System.Collections;
using UnityEngine;

public class Npc_MasterScript : MonoBehaviour,NpcInterface
{
    public struct Npc
    {
        public string name; //Npc name duh
        public int NpcId;// 
        public string Job; // 
        public Hashtable Inventory;


    }

    public Npc NpcData;
    


    void NpcInterface.DayStart() { }
    void NpcInterface.EndDay() { }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
