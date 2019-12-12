using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRTimeModifier : MonoBehaviour
{
    public TextMesh dateTextMeshMonth;
    public TextMesh dateTextMeshYear;
    public TextMesh dateTextMeshSpeed;
    public SteamVR_Action_Vector2 m_TouchPosition;
    //public Hand m_LftHand;
    //public Hand m_RgtHand;

    //private EVRButtonId touchpad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    //private SteamVR_TrackedObject trackedObj;

    private void Start()
    {

    }
    void Update()
    {
        float newSpeed = m_TouchPosition.axis.x * 0.1f;
        TimelineController.Instance.timeLineSpeed += newSpeed;

        dateTextMeshMonth.text = "Month: " + TimelineController.Instance.currentTimelineDate.month.ToString(); 
        dateTextMeshYear.text = "Year: " + TimelineController.Instance.currentTimelineDate.year.ToString(); 
        dateTextMeshSpeed.text = "Speed: " + TimelineController.Instance.timeLineSpeed.ToString(); 
    }
}
