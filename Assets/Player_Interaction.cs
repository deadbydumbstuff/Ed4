using UnityEngine;
using System.Collections.Generic;
//using System.linq;

public class Player_Interaction : MonoBehaviour
{
    public List<GameObject> interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input .getkeydown(interactionkey)
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
            foreach (Interactable I in ie)
            {
                Debug.Log(I);
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
