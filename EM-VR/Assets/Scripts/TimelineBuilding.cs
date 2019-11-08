using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineBuilding : MonoBehaviour
{
    public TimelineDate startConstructionTimelineDate;
    public TimelineDate endConstructionTimelineDate;

    public MeshRenderer buildingMesh;

    [SerializeField]
    public DateTime startConstructionDate;
    [SerializeField]
    public DateTime endConstructionDate;
    public AnimationCurve animCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.8f, 1.0f), new Keyframe(1.0f, 0.0f));

    private GameObject constructionBuilding;
    private Material constructionMaterialForMe; //based on each building for tile info
    // Start is called before the first frame update
    void Start()
    {
        startConstructionDate = new DateTime(startConstructionTimelineDate.year, startConstructionTimelineDate.month, startConstructionTimelineDate.day);
        endConstructionDate = new DateTime(endConstructionTimelineDate.year, endConstructionTimelineDate.month, endConstructionTimelineDate.day);
        constructionMaterialForMe = new Material( Resources.Load("Materials/ConstructionMaterial/ConstructionMaterial") as Material); //create copy?
    }

    // Update is called once per frame
    void Update()
    {
        //if the current time is within the construction date
        if(TimelineController.Instance.CurrentDate < endConstructionDate && TimelineController.Instance.CurrentDate > startConstructionDate)
        {
            if(constructionBuilding == null)
            {
                constructionBuilding = GameObject.CreatePrimitive(PrimitiveType.Cube);
                constructionBuilding.GetComponent<MeshRenderer>().material = constructionMaterialForMe;
                constructionBuilding.transform.parent = transform;
            }
            float relTime = get01FromDateTime();
            float amount = animCurve.Evaluate(relTime);
            Bounds cBounds = getBoundsForFloat01(amount);
            constructionBuilding.transform.localScale = cBounds.extents * 2;
            constructionBuilding.transform.position = buildingMesh.transform.position + cBounds.center;
            constructionBuilding.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if(TimelineController.Instance.timeLineSpeed > 0.0f)
            {
                if (relTime >= 0.8f)
                {
                    buildingMesh.enabled = true;
                }
            }
            else
            {
                if(relTime <= 0.8f)
                {
                    buildingMesh.enabled = false;
                }
            }
            
            constructionMaterialForMe.mainTextureScale = new Vector2(2.0f, amount);

        }
        else//destroy any construction materials
        {
            if(constructionBuilding != null)
            {
                Destroy(constructionBuilding);
                constructionBuilding = null;
            }
            if (TimelineController.Instance.CurrentDate < startConstructionDate)
            {
                buildingMesh.enabled = false;
            }
            else if (TimelineController.Instance.CurrentDate > endConstructionDate)
            {
                buildingMesh.enabled = true;
            }
        }

        
    }

    Bounds getBoundsForFloat01(float amount)
    {
        Bounds bounds = new Bounds();
        bounds.extents = new Vector3(buildingMesh.bounds.extents.x, buildingMesh.bounds.extents.y * amount, buildingMesh.bounds.extents.z);
        bounds.center = new Vector3(0, bounds.extents.y - 0.01f - (buildingMesh.bounds.extents.y), 0);
        bounds.Expand(new Vector3(0.5f,0.0f,0.5f));

        return bounds;
    }
    
    //only call if sure current date is within the bounds
    float get01FromDateTime()
    {
        DateTime currentTime = TimelineController.Instance.CurrentDate;
        TimeSpan distToCompletion = endConstructionDate - startConstructionDate;
        TimeSpan currentRelative = currentTime - startConstructionDate;
        return ((float)currentRelative.Ticks) / distToCompletion.Ticks;
    }
}

[Serializable]
public struct TimelineDate
{
    public TimelineDate(int month, int day, int year)
    {
        this.month = month;
        this.day = day;
        this.year = year;
    }

    public int month;
    public int day;
    public int year;
}
