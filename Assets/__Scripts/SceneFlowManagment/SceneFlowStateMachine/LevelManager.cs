using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Singleton pattern for LevelManager -- Referanslar
    [Header("Level-0 Referances")]
    [SerializeField] public CineMachineManager CineMachineManager;//sens ve açý kontrollü
    [SerializeField] public playerController playerController; // player controller referansý

    [SerializeField] public GameObject starterTimeline;
    [SerializeField] public GameObject firstQuestStater;
    





    public LevelStateMachine stateMachine = new();
    LevelState currentState;

    private void Start()
    {

        GameEvent.OnQuestCompleted += OnQuestComplete;
       

        stateMachine.ChangeState(new L0_StartState() , this);
    }

    public void OnQuestComplete(string questID) // bunu game eventteki evente abone ediyorum
    {
        Debug.Log("level managerdaki bir fonksiyonum"); // Log the quest completion
        //state deðiþtir  

        if(questID == "Level0-Start-Quest")
        {
            Debug.Log("State Deðiþikliði");
            starterTimeline.SetActive(false); // Starter timeline'ý aktif et
            stateMachine.ChangeState(new L0_FirstQuest());
            firstQuestStater.SetActive(true);

        }

        if (questID == "EscapeTheTrain")
        {

        }

    }

    private void Update()
    {
        if(stateMachine.currentState != null)
        {
           stateMachine.currentState?.Update(); // Update the current state
        }

    }



}
