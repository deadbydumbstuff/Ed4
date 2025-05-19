using Unity.Mathematics;
using UnityEngine;
using TMPro;
using System.Linq;

public class Time_Mananger : MonoBehaviour
{
    public static Time_Mananger instance; //<-- singlton
    public Week CurrentWeekDay;


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
        }
    }

    //debug.log($" {hour} : {Min} : {Secomd }")
    //debug.log($" {hour} : {Min update every 10}")

    // floor(min/10) * 10 


    public GameObject Clock;//display the daytime stuff :3

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
    public class Month
    {
        Week week1;
        Week week2;
        Week week3;
        Week week4;
        //public Events  events have a week and date in them 
    }
    public class Year
    {
        Month Spring;
        Month Summer;
        Month Autisum;
        Month Winter;
    }

    private void Update()
    {
        Second += 10 * Time.deltaTime;
        string TruncatedTime = ($"0{Minute}");
        Clock.GetComponent<TMP_Text>().text = $"{Hour} : {TruncatedTime[^2]}{TruncatedTime[^1]}";
    }


    public void DayEnd()
    {
        CurrentWeekDay += 1;
        if (CurrentWeekDay > Week.Sunday)
        {
            CurrentWeekDay = Week.Monday;
        }
        //when the day ends 
            //progress current day to next
            //call all endday functions 
            //update day/month/year count
        //set the time
        Hour = 9; Minute = 0; Second = 0;
    }
}
