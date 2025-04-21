using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //could add a sorta 
        if (collision.gameObject.GetComponent<Interactable>() != null)
        {
            //this item is interactable do the on enter function :3
            //open a menu like something
            Debug.Log("test");
            collision.gameObject.GetComponent<Interactable>().EnterRange();
        }
    }
}
