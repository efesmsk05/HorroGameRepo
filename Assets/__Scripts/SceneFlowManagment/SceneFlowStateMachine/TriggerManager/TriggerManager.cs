using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ProcessTrigger(TriggerData triggerData)
    {
        switch (triggerData.triggerType)
        {
            case TriggerType.TriggerTypes.PlayCutscene:
     
                break;

            case TriggerType.TriggerTypes.PlaySound:
                //playsound
                LevelSoundManager.instance.PlayFx(triggerData.soundClip);
                break;

            case TriggerType.TriggerTypes.Play3dSound:
                LevelSoundManager.instance.Play3DFx(triggerData.soundClip, triggerData.soundPosition);
                break;


            case TriggerType.TriggerTypes.ChangeEnvironment:// çevre deðiþtir
                break;

            case TriggerType.TriggerTypes.Subtitle:
                Debug.Log("Subtitle Triggered: " + triggerData.description);
                break;


            default:
                Debug.LogWarning("Unknown trigger type: " + triggerData.triggerType);
                break;
        }
    }
}
