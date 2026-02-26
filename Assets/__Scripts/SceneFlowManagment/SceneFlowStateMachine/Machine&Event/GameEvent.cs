using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class GameEvent 
{
    public static event Action<string> OnQuestCompleted; // Event for quest completion


    public static void QuestCompleted(string questID)
    {
        Debug.Log($"Quest {questID} completed! abone eventler çalýþtýrýlýyor");

        OnQuestCompleted?.Invoke(questID); // Invoke the event with the quest ID

    }


}
