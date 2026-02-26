using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "TriggerEventData", menuName = "Trigger System/Event Data")]
[CreateAssetMenu(fileName = "TriggerData" , menuName = "TriggerSystem/Data")]
public class TriggerData : ScriptableObject
{
    public string triggerID;
    public TriggerType.TriggerTypes triggerType;
    public string description;

    [Header ("For Sound Triggers")]
    public AudioClip soundClip;
    public Vector3 soundPosition; // For 3D sound triggers
    [Header("For CutScene Trigger")]
    public string cutsceneName;

}
