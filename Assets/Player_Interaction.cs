using UnityEngine;
using System.Collections.Generic;

public class Player_Interaction : MonoBehaviour
{
    private List<Interactable> interactable;
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
            Interactable ie = collision.gameObject.GetComponent<Interactable>();
            //this item is interactable do the on enter function :3
            //open a menu like something
            Debug.Log("test");
            ie.EnterRange(); // shows the propmt to interact
            interactable.Add(ie);
        }

       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable.Contains(collision.GetComponent<Interactable>() ) )
        {
            interactable.Remove(collision.GetComponent<Interactable>());
        }
        //if object in interaction list disable it //also check for tags in the firstplace
    }
}
