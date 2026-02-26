using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public Dictionary<string, Quest> activeQuests = new();// bellek açtýk bir hafýza gibi düþün 
    private void Awake()
    {
        Instance = this;
    }

    //quest Register

    public void RegisterQuest(Quest quest)
    {
        if(!activeQuests.ContainsKey(quest.questID))
        {

            Debug.Log($"Quest Registered: {quest.questID} - {quest.description}");
            activeQuests.Add(quest.questID , quest);
        }

    }
    //quest Active Quest
    public void ActiveQuest(string questID)
    {
        if(activeQuests.TryGetValue(questID , out var quest))// veri kayýtlýysa 
        {
            quest.onStart?.Invoke();
        }
  

    }

    //quest Competed Quest
    public void CompletedQuest(string questID)
    {
        if(activeQuests.TryGetValue(questID , out var quest) && !quest.isCompleted)// kayýtlýysa ve tamamalanmadýysa 
        {
            quest.isCompleted = true; 
            quest.onComplete?.Invoke(); 

            GameEvent.QuestCompleted(questID); // Quest completed event trigger

        }
    }

    // quest check

    public bool CheckQuest(string questID)
    {
        if(activeQuests.TryGetValue(questID , out var quests))
        {
            return quests.isCompleted; // Eðer kayýtlýysa ve tamamlandýysa true döner
        }

        return false; // Kayýtlý deðilse false döner
    }
}
