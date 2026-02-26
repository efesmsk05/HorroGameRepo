using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public TriggerData triggerData;

    public AudioSource source3dFx;
    public bool isOneTimeTrigger = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || (isOneTimeTrigger && hasTriggered))
            return;

        hasTriggered = true;
        TriggerManager.Instance.ProcessTrigger(triggerData);
    }
}
