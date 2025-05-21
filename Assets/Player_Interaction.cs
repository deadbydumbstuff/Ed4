using UnityEngine;
using System.Collections.Generic;
using System.Linq;
//using System.linq;

public class Player_Interaction : MonoBehaviour
{
    public Player_Core core;
    public List<GameObject> interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input .getkeydown(interactionkey)
        if (Input.GetKeyDown(core.interact) && interactable.Count > 0)  {
            GameObject interaction = interactable[0];
            foreach (GameObject obj in interactable) {
                //check distance
                if (Vector2.Distance(obj.transform.position, transform.position) < Vector2.Distance(interaction.transform.position, transform.position)) {
                    interaction = obj;
                }
            }
            Interactable[] If = interaction.GetComponents<Interactable>();
            foreach (var item in If)
            {
                item.Interact();
            }
        }
        //find the most appropriate interacatble object and run its function
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //could add a sorta 
        if (collision.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable[] ie = collision.gameObject.GetComponents<Interactable>();
            //this item is interactable do the on enter function :3
            //open a menu like something
            //Debug.Log("interacabke");
            foreach (Interactable I in ie)
            {
                //Debug.Log(I);
                I.EnterRange(); // shows the propmt to interact
                interactable.Add(collision.gameObject);
            }
    
        }

       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable.Contains(collision.gameObject) )
        {
            collision.gameObject.GetComponent<Interactable>().ExitRange();
            interactable.Remove(collision.gameObject);
        }
        //if object in interaction list disable it //also check for tags in the firstplace
    }

}
