using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class NpcBehaviour : MonoBehaviour, NpcPathFinding
{
    //this script is for more precives behaviourds and how they effect the npc stuff like day plans
    // could have a enums
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Npc_MasterScript masterScript;
    Dictionary<Vector2, NpcPathFinding.Direction> Path = new();
    //KeyValuePair<Vector2, NpcPathFinding.Direction> CurrntDir;
    public Vector2 goalPos;
    int i;
    public GameObject testobect;
    public NpcPathFinding.Direction dir;
    void Start()
    {
        Path = masterScript.GeneratePath(goalPos, transform.position);

        dir = Path.Values.Last();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        i++;
        if (i >= 10)
        {
            i = 0;
            //get currnt direction move to it get its vecotr check vector in dic hope and pra
            switch(dir)
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
                case NpcPathFinding.Direction.End://reached goal
                    //Debug.Log("end");
                    break;
            }
            Vector2 pos = transform.position;
            Path.TryGetValue(pos,out dir);
            //Debug.Log(Path[pos]);
            
        }
    }

    void PathFind()
    {
        //get current point look for it in index and work backwards?
        
    }

}
