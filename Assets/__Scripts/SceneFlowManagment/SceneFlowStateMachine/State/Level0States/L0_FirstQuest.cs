using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0_FirstQuest : LevelState
{
    public override void Enter()
    {

        Debug.Log("state 0 girdi görevler verilicek ve demo baþlar");
        TimelineManager.Instance.OnIntroFinished();// start game cut scene (kamera geriye doðru gider

        QuestManager.Instance.RegisterQuest(new Quest
        {
            questID = "Level0-First-Quest",
            description = "NOT DEFTERÝNDEKÝ GÖREVLERÝ TAMAMLA",
            onStart = () =>
            {

                //GÖREV ALINDI SESÝ
                //AudioManager.Instance.PlaySound("QuestAccepted"); ÖRNEK SES KODU

                QuestUiManager.Instance.AddSubQuest(new SubQuest
                {
                    subQuestID = "Edit_Repository",
                    subQuestUiText = "Deopyu Düzenlemeliyim",
                    subQuestMainQuestHeader = "Günlük Görevler",



                });

                QuestUiManager.Instance.AddSubQuest(new SubQuest
                {
                    subQuestID = "Explore_The_Metro",
                    subQuestUiText = "Günlük Vardiyaný Tamamla",
                    subQuestMainQuestHeader = "Günlük Görevler",



                });



            },
            onComplete = () =>
            {
                // GÖREV TAMAMLANDI SESÝ

            }
        });

        QuestManager.Instance.ActiveQuest("Level0-First-Quest");

    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        Debug.Log("L0_StartState: Exiting the start state of Level 0.");

    }

}
