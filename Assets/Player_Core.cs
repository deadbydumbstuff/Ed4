using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// player core will savbe all the infromation the player has
/// all player inputted information such as name. and other details
/// health and money
/// speed vaules
/// will also be the interaction core
/// </summary>
public class Player_Core : MonoBehaviour
{
    public static Player_Core instance;
    [Header("Core")]
    public string Name;
    public float Gold;
    //what other information does the player contain

    [Header("Movement")]
    public float Speed;


    [Header("Interaction")]
    public KeyCode interact;
    public float Range;
    //list of items
    // somethging else
    //based of mouse postion choose the closest interaction

    [Header("Inventory")]
    public Player_Inventory inventory;
    //s

    [Header("Dialouge")]
    public KeyCode AdvanceDialouge;
    //textspeed//font//size//auto scroll

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        //check for nearby interactable objects if in range allow an input to be done
    }

    //interaction zone
    //export important info into a save file such as potions.
    void Save()
    {

    }
}
