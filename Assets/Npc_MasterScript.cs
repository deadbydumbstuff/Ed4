using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Npc_MasterScript : MonoBehaviour,NpcInterface
{
    public class Npc
    {
        public string name; //Npc name duh
        public int NpcId;// 
        public string Job; // 
        
        public Dictionary<int,int> Inventory;


    }



    public Npc NpcData;

    public NpcTraits[] NpcTrait;
    
    public enum NpcTraits
    {
        Friendly,
        Allergic,
        Weird,
        Lonely
    }


    [Header("A* Pathfinding")]
    [SerializeField]TileBase TileBase;
    [SerializeField]List<TileMapData> TileData;
    [SerializeField] Tilemap tilemap;
    Dictionary<TileBase,TileMapData> TileVaule;
    //could make this into a hastable as i only need to read the tilemapdata types vaule?
    [SerializeField] int gvaule;

    //open lisy
    //private HashList hashList;
    #region  InterfaceFunc
    void NpcInterface.DayStart() { }
    void NpcInterface.EndDay() { }
    #endregion

    void Awake() 
    {
        CreateDiction();
        Debug.Log((int)TileVaule[TileBase].type);

    }
    /// <summary>
    /// creates a dictionary of every single type of tile used in the npc pathing algorthim and their tilemapdata so we can use the tiletype as the key and it will return its tiledata scritable oject 
    /// </summary>
    void CreateDiction()
    {
        TileVaule = new Dictionary<TileBase, TileMapData>();
        foreach(var tileMapData in TileData)
        {
            foreach(TileBase tileType in tileMapData.tiles)
            {
                TileVaule.Add(tileType, tileMapData);
            }
        }
        //
    }
    #region A*
    //  https://www.youtube.com/watch?v=mZfyt03LDH4&list=PLFt_AvWsXl0eTHFZ2XPkM6gLK8XdsdzNl&index=12
    //  https://www.youtube.com/watch?v=3Dw5d7PlcTM&list=PLFt_AvWsXl0eTHFZ2XPkM6gLK8XdsdzNl&index=28
    //  https://www.youtube.com/watch?v=dn1XRIaROM4&list=PLFt_AvWsXl0eTHFZ2XPkM6gLK8XdsdzNl&index=39


    //vaules 

    //ASTAR METHOD 
    //GVAULE
    //FVAULE
    //CURRENT VAULE 
    //get nabours if nabours have been checkd add them ot a haslish for quick checking of vaules 
    //get the start pos and the end pos and translate them into grid positions
    //get the start pos and check for the end pos 
    //calc the goal vaule or smt
    //get nabours add them to open list 
    //check closes nabours for path and more naoburs
    //after checking nabours add checked nabours into
    //add check tiles into the closed hashlist
    //check next closest nabour if its in the hashlist and doa boogie woogie
    //if its not in the list check it and its nabours if nabours arnt in haslist add them to open list 
    //check the open list and funk

    //it might be better to run the a* in a struct as once the path is created we output all the data and remove the unsesed data

    /*struct path
    {
        public Vector2;
        public Vector2;
        public Tilemap;
        public Dictionary<>;

    }*/


    void GeneratePath(Vector2 StartPos,Vector2 Goal)
    {
        
    }
    #endregion
}
