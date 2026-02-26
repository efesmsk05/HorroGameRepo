using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTheTrain : LevelState
{
    public static string[] escapeTheTrainSubQuests = new string[] // sub quests
    {
        "FindSafePlace"
    };
    public override void Enter()
    {
        Debug.Log("Entering EscapeTheTrain: Player needs to escape the train before it crashes.");
        Quest quest = new Quest
        {
            questID = "EscapeTheTrain",
            description = "Escape the train before it crashes.",
            onStart = () =>
            {
                QuestUiManager.Instance.AddSubQuest(new SubQuest
                {
                    subQuestID = "FindSafePlace",
                    subQuestUiText = "Find a safe place to escape the train [ ]",

                });

            },
            onComplete = () =>
            {
            }
        };


        QuestManager.Instance.RegisterQuest(quest);//kayýt ol 
        QuestManager.Instance.ActiveQuest(quest.questID);// aktif et


    }
    public override void Update()
    {
    }

    public override void Exit()
    {

    }
}
