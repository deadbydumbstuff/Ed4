using System;
using UnityEngine;

public class Npc_EventTest : MonoBehaviour
{

    public class Event : TimeBasedEvent
    {
        public void EventTrigger()
        {
            throw new System.NotImplementedException();
        }
    }
    public class EvilEvent : TimeBasedEvent
    {
        public void EventTrigger()
        {
            //throw new System.NotImplementedException();
            Debug.Log("twekrk");
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EvilEvent evil = new EvilEvent();
        TimeBasedEvent e = evil;
        Time_Mananger.instance.CreateNewEvent(evil, Time_Mananger.instance.CurrentDate, 8, 00);
    }

    // Update is called once per frame

}
