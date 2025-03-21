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
    private Vector2 nextpos;
    void Start()
    {
        Path = masterScript.GeneratePath(transform.position, goalPos);
        goalPos = new Vector2(Mathf.Round(goalPos.x), Mathf.Round(goalPos.y));
        //dir = Path.Values.Last() - Path.Keys.Last();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(vector3.x, vector3.y);
            Debug.Log(mousePos);
            mousePos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            goalPos = mousePos;
            Path = masterScript.GeneratePath(transform.position,mousePos);
        }



        i++;
        if (i >= 10 && Path != null && new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y)) !=  goalPos)
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
            
        }

    }

    void PathFind()
    {
        //get current point look for it in index and work backwards?
        
    }

}

