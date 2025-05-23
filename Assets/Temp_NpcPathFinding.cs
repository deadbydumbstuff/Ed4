using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Temp_NpcPathFinding : MonoBehaviour
{
    public float NpcFollowRange;
    public bool PathFind;

    

    float i;
    public Vector2 goalPos;
    //public Npc_MasterScript masterScript;
    public Dictionary<Vector2, NpcPathFinding.Direction> Path = new();
    NpcPathFinding.Direction dir;

    public StoryProgression SP;
    public enum StoryProgression 
    {
        firstConcerstation,
        EnterHome,
        gathering_cure,
        compleat,
    }

    public GameObject ColliderAskHelp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColliderAskHelp.active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PathFind )
        {
            i++;
            if (i >= 10 && Vector2.Distance(this.gameObject.transform.position, Player_Core.instance.transform.position) <= NpcFollowRange && Path != null && new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)) != goalPos)
            {
                i = 0;
                //Path.TryGetValue(transform.position,out nextpos);
                //dir = NpcPathFinding.Direction.Left;
                Vector2 pos = transform.position;
                Path.TryGetValue(pos, out dir);
                switch (dir)
                {
                    case NpcPathFinding.Direction.Left://go right
                        transform.position -= Vector3.right;
                        break;
                    case NpcPathFinding.Direction.Right://go left
                        transform.position -= Vector3.left;
                        break;
                    case NpcPathFinding.Direction.Up://go down
                        transform.position -= Vector3.down;
                        break;
                    case NpcPathFinding.Direction.Down://go up
                        transform.position -= Vector3.up;
                        break;
                }
                //Debug.Log(Path[pos]);
                if ((Vector2)transform.position == goalPos)
                {
                    if (SP == StoryProgression.firstConcerstation)
                    {
                        SP = StoryProgression.EnterHome;
                        //(-66, -20, 0)
                        transform.position = new(-66, -20, 0);
                        //new goal pathfind
                        Path = Npc_MasterScript.instance.GeneratePath(transform.position, new(-59, -18));
                        goalPos = new(-59, -18);
                        //enter the home
                        //UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-58.6062126159668,"y":-17.063827514648439,"z":0.0},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":3.4502501487731935,"y":3.4502501487731935,"z":1.3801000118255616}}
                        //bs
                    }
                    else if (SP == StoryProgression.EnterHome)
                    {
                       // Debug.Log("Bed");
                        //creat the trigger box to say text 
                        ColliderAskHelp.active = true;
                    }
                }
                    //teleport to the inside of the building and pathfind to the next location
                

            }

        } 
    
    }
}
