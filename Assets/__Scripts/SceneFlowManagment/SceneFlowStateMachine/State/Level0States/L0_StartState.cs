using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0_StartState : LevelState
{

    public override void Enter()
    {
        Debug.Log("Level-0 Start -- Karater masada oturuyor ");

        playerController.instance.enabled = false; // Player controller'ý devre dýþý býrak


        #region Camera Settings
        levelManager.CineMachineManager.maxAngleY = 25f; // Set the maximum camera angle
        levelManager.CineMachineManager.minAngleY = -45f; // Set the minimum camera angle
        levelManager.CineMachineManager.maxAngleX = 225f; // Set the maximum camera angle
        levelManager.CineMachineManager.minAngleX = 135f; // Set the minimum camera angle

        levelManager.CineMachineManager.isCutScene = true; // Set the camera to cutscene mode
        levelManager.CineMachineManager.cameraAcceleration = 15; // manuel 25 
        #endregion

        QuestManager.Instance.RegisterQuest(new Quest
        {
            questID = "Level0-Start-Quest",
            description = "Welcome to Level 0! Take The Notepad",
            onStart = () =>
            {
                Debug.Log("Starting the Level 0 quest. I need to look at the notebook");
                // starter character Subtatle "I need to look at the notebook"
                // Add any UI or initial setup here
            },
            onComplete = () =>
            {
                Debug.Log("Level 0 quest completed.");
                // Transition to the next state or level
            }
        });

        QuestManager.Instance.ActiveQuest("Level0-Start-Quest");

    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        Debug.Log("L0_StartState: Exiting the start state of Level 0.");
        levelManager.CineMachineManager.maxAngleY = 80f; 
        levelManager.CineMachineManager.minAngleY = -70f; 
        levelManager.CineMachineManager.isCutScene = false;
        levelManager.CineMachineManager.cameraAcceleration = 25f;



    }



}
