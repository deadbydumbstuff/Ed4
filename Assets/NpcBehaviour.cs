using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [Header("A* Pathfinding")]
    [SerializeField]int gvaule;
    [SerializeField]TileMapData tilemap;
    //open lisy
    //private HashList checkedNabours;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    




}
