using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using TMPro;
using UnityEngine.UIElements;

public interface NpcPathFinding
{
    public enum Direction
    { Left = 0, Right = 1, Up = 2, Down = 3, End = 4}
}

public class Npc_MasterScript : MonoBehaviour,NpcPathFinding
{
    [Header("Debug Settings")]
    [SerializeField]private bool Debuging;
    [SerializeField] GameObject DebugPoint;
    [SerializeField] TileBase TileBase;

    [Header("A* Pathfinding")]
    [SerializeField]List<TileMapData> TileData;
    [SerializeField] Tilemap tilemap;
    Dictionary<TileBase,TileMapData> TileVaule;
    //could make this into a hastable as i only need to read the tilemapdata types vaule?
    [SerializeField] int gvaule;

    //open lisy dic
    // vector2 position , int fcost    ---  might need to change this int to something to store more data such as gcose and h cose? idk tho 
    //Dictionary<Vector2,int> OpenList;
    //private Hashset of vector 2 pos hashList;
    //HashSet<Vector2> ClosedList;

    void Awake() 
    {
        CreateDiction();
        //Debug.Log((int)TileVaule[TileBase].type);

        //Debug.Log(GetTileVaule(new Vector2(0,0)));
    }

    private void Update()
    {
        #region Debug /MouseDebug
        if (Debuging && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Debug.Log(GetTileVaule(mousePos));
        }
        #endregion
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

    #region yap
    //vaules 

    //ASTAR METHOD 
    //G Vaule   distance from start
    //H vaule   distance from end node in directed pathing   if we do 4 way travle its just x * c + y* c c being the avrage walk cost? 
    //F vaule   total of G+H vaule
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
    #endregion
    struct path // on call create a struct with the npc and its path
    {
        Dictionary<Vector2, int> OpenList;
        Dictionary<Vector2, Vector2> ParentsList;
        HashSet<Vector2> ClosedList;
        Vector2 StartPos;
        Vector2 EndPos;
    }
    /// <summary>
    /// create a path using the A* pathfinding 
    /// </summary>
    /// <param name="StartPos"></param> the postion the npc starts from
    /// <param name="Goal"></param> the goal postion to creat the path from
    public Dictionary<Vector2, NpcPathFinding.Direction> GeneratePath(Vector2 StartPos,Vector2 Goal)
    {
        StartPos = new Vector2(Mathf.Floor(StartPos.x), Mathf.Floor(StartPos.y));
        Goal = new Vector2(Mathf.Floor(Goal.x), Mathf.Floor(Goal.y));
        Dictionary<Vector2, int> OpenList = new();  //v2 = pos , int = tilecost + f cost 
        //create a hashtable of the node and its parent hashtable<vector2,vector2> where ther first one is its noad as each key uqiue and
        Dictionary<Vector2,NpcPathFinding.Direction> ParentsList = new();
        HashSet<Vector2> ClosedList = new();

        int F = HGcost(StartPos, Goal);
        OpenList.Add(StartPos,F);
        ParentsList.Add(StartPos, NpcPathFinding.Direction.End);
        Vector2 Point = StartPos;
        bool GoalFound = false;
        while (OpenList.Count > 0 && !GoalFound) {
            int p = 643029;
            foreach (var kvp in OpenList)
            {
                if (kvp.Value < p)
                {
                    p = kvp.Value;
                    Point = kvp.Key;
                }
                if (kvp.Value == p && (GetTileVaule(kvp.Key) + HGcost(kvp.Key, Goal)) <= (GetTileVaule(Point) + HGcost(Point, Goal)))
                {
                    p = kvp.Value;
                    Point = kvp.Key;
                }
            }


            List<Vector2> nabs = new()
            {
                //get nabs
                new(Point.x - 1, Point.y),  //left of center
                new(Point.x + 1, Point.y),  //right of center
                new(Point.x, Point.y + 1),  //up of center
                new(Point.x, Point.y - 1)   //down of center
            };
            //cal the cost of each
            int iteration = -1;
            foreach (Vector2 nab in nabs)
            {
                if (GoalFound) { break; }
                iteration++;
                int quickInt;
                OpenList.TryGetValue(nab, out quickInt);
                if (GetTileVaule(nab) != -1 && !ParentsList.Keys.Contains(nab))
                {
                    if (nab == Goal)
                    {
                        OpenList.Clear();
                        ParentsList.Add(nab, NpcPathFinding.Direction.Left+iteration);

                        GoalFound = true;
                    }
                    else {
                        int cost = GetTileVaule(nab) + (HGcost(nab, StartPos) + HGcost(Goal, nab));
                        ParentsList.Add(nab, NpcPathFinding.Direction.Left + iteration);
                        OpenList.Add(nab, cost);
                    }
                }
                else
                {
                    ClosedList.Add(nab);
            

                }
            }
            nabs.Clear();
            //we remove point as its a unique to each coord and wont reapaire 
            OpenList.Remove(Point); //make sure we dont check this point again
            ClosedList.Add(Point);//make sure nabours dont readd this point again
                                  // now we got an addest list of the nabours of the closest tile
        }
        if (GoalFound) //reverssort the path
        {
            GoalFound = false;
            Vector2 pos = Goal;
            NpcPathFinding.Direction Dir = ParentsList[Goal];
            while (!GoalFound)
            {
                if (pos == StartPos)
                {
                    //ParentsList[pos] = Dir;
                    return ParentsList;
                }
                switch (Dir)
                {
                    case NpcPathFinding.Direction.Left:
                        pos -= Vector2.left;
                        break;
                    case NpcPathFinding.Direction.Right:
                        pos -= Vector2.right;
                        break;
                    case NpcPathFinding.Direction.Up:
                        pos -= Vector2.up;
                        break;
                    case NpcPathFinding.Direction.Down:
                        pos -= Vector2.down;
                        break;
                    case NpcPathFinding.Direction.End: //start pos

                        break;
                        
                }
                NpcPathFinding.Direction tempDir;
                ParentsList.TryGetValue(pos, out tempDir);
                ParentsList[pos] = Dir;
                Dir = tempDir;
                //ParentsList
                //get the pos by following the direction to get its prev  
                //  get the enum of that pos <-- store this
                //  replace pos enum with
                //  
            }
            ParentsList[Goal] = NpcPathFinding.Direction.End;//replace the goal pos with end
        }
        else { Debug.Log("No PATH"); }
        return ParentsList;
    }


    int HGcost(Vector2 CurrntPos,Vector2 Goal)
    {
        int x = Mathf.Abs((int)Goal.x - (int)CurrntPos.x);
        int y = Mathf.Abs((int)Goal.y - (int)CurrntPos.y);
        return x + y;
    }

    /// <summary>
    /// given a coordiant we check if that tile is on the tilemap and its vaule to step on
    /// .public beacuse npc will use this to get the tile they stand on and change speed accordingly
    /// .-1
    /// </summary>
    /// <param name="Postion"></param>
    public int GetTileVaule(Vector2 Postion)
    {
        Vector3Int V3I = new Vector3Int((int)Mathf.Round(Postion.x),(int)Mathf.Round(Postion.y),0);
        TileBase Tb = tilemap.GetTile(V3I);
        return Tb ? (int)TileVaule[Tb].type : -1;
    }


    #endregion
}
