using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    private static TimelineController instance;
    public static TimelineController Instance { get { return instance; } }

    public float timeLineSpeed = 10.0f; //in days
    public bool Debugging = false;
 

    public TimelineDate currentTimelineDate;
    private DateTime currentDate;
    public DateTime CurrentDate { get { return currentDate; } }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentDate = new DateTime(currentTimelineDate.year, currentTimelineDate.month, currentTimelineDate.day);
    }

    // Update is called once per frame
    void Update()
    {
        DateTime lastCurrentDate = currentDate; //to detect changes
        currentDate = new DateTime(currentTimelineDate.year, currentTimelineDate.month, currentTimelineDate.day);
        if (timeLineSpeed >= 1.0f || timeLineSpeed <= -1.0f)
        {
            currentDate = currentDate.AddDays(timeLineSpeed);
        }
        else
        {
            currentDate = currentDate.AddHours(24 * timeLineSpeed);
        }
        
        currentTimelineDate = new TimelineDate(currentDate.Month, currentDate.Day, currentDate.Year);
    }
}
