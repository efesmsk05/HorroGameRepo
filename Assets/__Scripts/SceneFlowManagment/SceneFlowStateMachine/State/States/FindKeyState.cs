using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindKeyState : LevelState
{
   public static string[] findKeyStateSubQuests = new string[] // sub quests
    {
        "FindKeyObject",
        "FindBoxObject"

    };
    public override void Enter()
    {

        Quest quest = new Quest
        {
            questID = "FindKey-MainQuest",
            description = "Find the key to unlock the door.",

            onStart = () =>
            {
                QuestUiManager.Instance.AddSubQuest(new SubQuest
                {
                    subQuestID = "FindKeyObject",
                    subQuestUiText = "Find the key to unlock the door [ ]",
                    subQuestMainQuestHeader = "Find Key",


                });

                QuestUiManager.Instance.AddSubQuest(new SubQuest
                {
                    subQuestID = "FindBoxObject",
                    subQuestUiText = "Find the box [ ]",
                    subQuestMainQuestHeader = "Find Key",



                });

            },

            onComplete = () =>
            {
                Debug.Log("Quest 'FindKey' has been completed.");


            }
        };

        QuestManager.Instance.RegisterQuest(quest);
        QuestManager.Instance.ActiveQuest(quest.questID);
    }

    public override void Update()
    {


        if (QuestUiManager.Instance.SubQuestCheck(FindKeyState.findKeyStateSubQuests))// sub quest konrol
        {
            QuestManager.Instance.CompletedQuest("FindKey-MainQuest");

            levelManager.stateMachine.ChangeState(new EscapeTheTrain()); // State deðiþtir

        }
    }
    public override void Exit()
    {
        QuestUiManager.Instance.RemoveSubQuestUi("FindKeyObject");
        QuestUiManager.Instance.RemoveSubQuestUi("FindBoxObject");

        LevelSoundManager.instance.PlayFx(LevelSoundManager.instance.levelSFX);


    }

}
