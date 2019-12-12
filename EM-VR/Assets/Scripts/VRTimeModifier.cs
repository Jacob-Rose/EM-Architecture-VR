using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRTimeModifier : MonoBehaviour
{
    public TextMesh dateTextMesh;
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
        float newSpeed = m_TouchPosition.delta.x;
        TimelineController.Instance.timeLineSpeed += newSpeed;

        dateTextMesh.text = TimelineController.Instance.currentTimelineDate.day.ToString() + " " 
            + TimelineController.Instance.currentTimelineDate.month.ToString() + " " 
            + TimelineController.Instance.currentTimelineDate.year.ToString() + " "
            + TimelineController.Instance.timeLineSpeed.ToString();
    }
}
