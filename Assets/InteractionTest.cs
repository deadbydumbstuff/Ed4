using UnityEngine;

public class InteractionTest : MonoBehaviour,Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//when the plaer is interacted 
    {
        //just do whatever i want when interacted
        throw new System.NotImplementedException();
    }

    public void EnterRange() // when the player is close enough to the object //could make this a basic function and polumorhisisit
    {
        //create a popup to show thi ssitem is interactable
        //edit the sprit to have an outline? shaders?????
        //track 
        Debug.Log("test");
    }
}
