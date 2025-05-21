using Unity.Mathematics;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

public interface TimeBasedEvent 
{
    void EventTrigger();
}
public class Time_Mananger : MonoBehaviour
{
    public static Time_Mananger instance; //<-- singlton-+
    public Date CurrentDate = new Date {day = Week.Thursday, week = Month.week2,Season = Season.Autisum,Year = 13}; 

    float second;
    public float Second { get { return second; } 
        set 
        { 
            second = value;
            if (second >= 45)
            {
                Minute += 10;
                second = 0;
            }
                  
        }
    } // get set if graater then 60 move to next time
    float minute;
    public float Minute
    {
        get { return minute; }
        set
        {
            minute = value;
            if (minute >= 60)
            {
                Hour += 1;
                minute = 0;
            }
            if (HoursEvents.Count <= 0) { return; }
            foreach (Event o in HoursEvents)
            {
                if (o.minite == minute)
                {
                    o.TBE.EventTrigger();
                }
            }
        }
    }
    float hour = 8;
    public float Hour
    {
        get { return hour; }
        set
        {
            hour = value;
            if (hour >= 24)
            {
                //day end
            }
            if (TodayEvents.Count <= 0) { return; }
            foreach (Event o in TodayEvents)
            {
                if (o.Hour == minute)
                {
                    TodayEvents.Remove(o);
                    HoursEvents.Add(o);

                }
            }
        }
    }
    //debug.log($" {hour} : {Min} : {Secomd }")
    //debug.log($" {hour} : {Min update every 10}")

    // floor(min/10) * 10 
    //public Dictionary<float, TimeBasedEvent> KVP;
    //using a minute and hour timestamp when the hour is selected add the minute to the

    public List<Event> EventQueue = new(); // the global que of all events 
    List<Event> TodayEvents = new();//
    List<Event> HoursEvents = new();//
    //todays events 
           //this hours events
                //this time = start events
    //when an event is added 

    // an dictonary of events 
    public class Event 
    {
        //day/month/year
        public Date date;
        public float Hour;//8-24
        public float minite; // 0,10,20,30,40,50
        public TimeBasedEvent TBE; // the function to run at the end of the 
        //replace <-- tbe wiht gameobject if i cant just add any interfaceimplamented function
    }

    public void CreateNewEvent(TimeBasedEvent Event, Date Date, float Hour, float Min)
    {
        //check what que iw would need to be in
        Event L = (new Event
        {
            date = Date,
            Hour = Hour,
            minite = Min,
            TBE = Event
        });

       //EventQueue.Add(L);
        if (CurrentDate == Date)
        {
            Debug.Log("Event is today");
            if (hour == Hour)
            {
                if (Min == minute)
                {
                    //do even
                    Debug.Log("Event is right now");
                    Event.EventTrigger(); //DO THE EVENT KRONK .............. oh wait this was the right lever good job kronk
                }
                else if (Min > minute)
                {
                    HoursEvents.Add(L);
                }
                else
                {
                    Debug.Log("Event has passed we missed it");
                }
            }
            else if (hour < Hour)
            {
                TodayEvents.Add(L);
            }
            else
            {
                //eventmissed
                Debug.Log("Event has passed we missed it");
            }


        }
        else // check if the date has passed
        {
            Debug.Log("eeee");
            if ( HasDatePassed(Date))
            {
                //date has passed 
            }
            else
            {
                EventQueue.Add(L);
            }
        }

        //check if the event is today/hour/now
    }

    bool HasDatePassed(Date date)
    {
        //check year
        if (date.Year <= CurrentDate.Year) // the year is a lower year
        {
            //check if year
            if (date.Season <= CurrentDate.Season) // the season is
            {
                //if day?
                if (date.week <= CurrentDate.week)
                {
                    if (date.day <= CurrentDate.day)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public GameObject Clock;//display the daytime stuff :3
    public GameObject DateDisplay;

    public enum Week
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
   // public Week week;
    public enum Month
    {
       week1,
       week2,
       week3,
       week4
        //public Events  events have a week and date in them 
    }
    public enum Season
    {
        spring,
        Summer,
        Autisum,
        Winter,
    }
    [System.Serializable]
    public class Date
    {
        public Week day;
        public Month week; //the week of the current month 1-4
        public Season Season;
        public float Year;
    }


    private void Update()
    {
        Second += 10 * Time.deltaTime;
        string TruncatedTime = ($"0{Minute}");
        Clock.GetComponent<TMP_Text>().text = $"{Hour} : {TruncatedTime[^2]}{TruncatedTime[^1]}";
        DateDisplay.GetComponent<TMP_Text>().text = $"{(((int)(CurrentDate.week)) * 7) + (int)CurrentDate.day + 1} : {CurrentDate.day} : {CurrentDate.Season} : {CurrentDate.Year}";
    }


    public void DayEnd()
    {
        Debug.Log("End Day");


        CurrentDate.day += 1;
        if (CurrentDate.day > Week.Sunday)
        {
            CurrentDate.day = Week.Monday;
            CurrentDate.week += 1;
            if (CurrentDate.week > Month.week4)
            {
                CurrentDate.week = Month.week1;
                CurrentDate.Season += 1;
                if (CurrentDate.Season >  Season.Winter)
                {
                    CurrentDate.Season = Season.spring;
                    CurrentDate.Year += 1;
                }
            }
        }
        //check the global events and if they match the date
        if (EventQueue.Count > 0)
        {


            foreach (Event o in EventQueue)
            {
                if (o.date == CurrentDate)
                {
                    EventQueue.Remove(o);
                    TodayEvents.Add(o);
                }
            }
        }

        //when the day ends 
            //progress current day to next
            //call all endday functions 
            //update day/month/year count
        //set the time
        Hour = 9; Minute = 0; Second = 0;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Debug.Log(EventQueue);
        //Debug.Log(TodayEvents);
       // Debug.Log(HoursEvents);
    }
}
