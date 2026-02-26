using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUiManager : MonoBehaviour
{


    [Header("Quest Ui Referances")]
    [SerializeField] private GameObject subquestReferance;
    [SerializeField] private TextMeshProUGUI mainQuestHeaderText;
    [SerializeField] private TextMeshProUGUI SubQuestText;
    [SerializeField] private GameObject completedIcon;



    [SerializeField] private GameObject mainQuestPanel;
    public static QuestUiManager Instance { get; private set; }

    public Dictionary<string, SubQuest> subQuests = new(); // Dictionary to hold subquests
    private void Awake()
    {
        Instance = this;
    }

    //first method --> show quest line ui 
    // asýl amacý bir textmeshi açmak ve quest line'ý göstermek
    public void ShowQuestUi(string subQuestID)
    {


        if (subQuests.TryGetValue(subQuestID, out var subQuest))
        {
            subQuest.textReferance = SubQuestText; // Set the reference to the text component
            var newSubQuestText = Instantiate(subQuest.textReferance, mainQuestPanel.transform);
            newSubQuestText.text = subQuest.subQuestUiText; // Set the text of the new subquest
            subQuest.textReferance = newSubQuestText; // Update the reference to the new text component
            newSubQuestText.gameObject.SetActive(true); // Activate the new subquest text
        }
        else
        {
            Debug.LogWarning($"SubQuest with ID {subQuestID} not found.");
        }




    }

    public void AddSubQuest(SubQuest subQuest )
    {
        if(!subQuests.ContainsKey(subQuest.subQuestID)) // önceden eklendimi
        { 
            Debug.Log($"SubQuest Registered: {subQuest.subQuestID} - {subQuest.subQuestUiText}");
            subQuests.Add(subQuest.subQuestID, subQuest); // Add the subquest to the dictionary
            ShowQuestUi(subQuest.subQuestID);
        }

    }

    //second method --> complete quest line ui

    public void CompleteQuestUi(string subQuestID)
    {
        if(subQuests.TryGetValue(subQuestID, out var subQuest) && !subQuest.isCompleted)// görev tamamlandým ve varmý kontrolü
        {
            // text üzerine tik ya da kutucuk iþaretlemesi yap
            subQuest.subQuestUiText += "[X]"; // Append "[Completed]" to the subquest text
            subQuest.textReferance.text = subQuest.subQuestUiText; // Update the text in the UI 
            subQuest.isCompleted = true;

        }
    }

    // third method --> hide quest line ui

    public void RemoveSubQuestUi(string subQuestID)
    {
        if(subQuests.TryGetValue(subQuestID , out var subQuest)) // varsa ve tamamlanmadýysa
        {
            subQuest.textReferance.gameObject.SetActive(false);
        }

    }

    public bool SubQuestCheck(string[] questID)
    {
        for (int i = 0; i < questID.Length; i++)
        {
            if (!subQuests.TryGetValue(questID[i], out var quest) || !quest.isCompleted)
            {
                return false; // Biri bile tamamlanmadýysa false döner
            }
        }
        return true; // Hepsi tamamlandýysa true döner
    }
}
