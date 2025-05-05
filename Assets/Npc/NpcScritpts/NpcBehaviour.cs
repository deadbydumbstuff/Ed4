using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Task
{ 
     //location
     //time--
     //duration
     //goal
     //reuierments
     //interactions

}




public class NpcBehaviour : MonoBehaviour, NpcPathFinding
{
    [Header("Debug")]
    [SerializeField] bool DebugMode;
    public Vector2 goalPos;
    //this script is for more precives behaviourds and how they effect the npc stuff like day plans
    // could have a enums
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Core")]
    public Npc_MasterScript masterScript;
    Dictionary<Vector2, NpcPathFinding.Direction> Path = new();

    [Header("Movement")]
    int i;
    NpcPathFinding.Direction dir;

    void Start()
    {
        Path = masterScript.GeneratePath(transform.position, goalPos);
        goalPos = new Vector2(Mathf.Round(goalPos.x), Mathf.Round(goalPos.y));
        //dir = Path.Values.Last() - Path.Keys.Last();

    }

    void Update()
    {
        #region DebugSettings
        if (Input.GetMouseButtonDown(0) && DebugMode)
        {

            Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(vector3.x, vector3.y);
            Debug.Log(mousePos);
            mousePos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            goalPos = mousePos;
            Path = masterScript.GeneratePath(transform.position, mousePos);
        }
        #endregion
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO IN MOVEMENT
        //Make it smooth movment instead of teleporting
        #region Movement
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
        #endregion


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if colliding with a interaction object and if that object is one the npc currently interacts with
    }

}

