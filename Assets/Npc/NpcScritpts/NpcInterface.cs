using System.Collections;
using UnityEngine;

public interface NpcInterface
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void DayStart()
    {
        Debug.Log("Test");
    }

    void EndDay();
}

public interface NpcJobInterface
{
    void Job();//used for each job

    void Event(); // add conditions for each event

    //what other important events would  i need 
    // void SendMsg();//send import commands to another villager //add a  
    // void ReciveMsg();//do stuff when reciving information from another village

    void Tick();
}
