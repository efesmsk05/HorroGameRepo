using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    [SerializeField] public  TMP_Text interactionText;
    [SerializeField] public  TMP_Text collectedFuelText;
    [SerializeField] public TMP_Text collectedPillText;
    [SerializeField] public TMP_Text collectedBatteryText;


    private void Awake()
    {
        instance = this;
    }



    public void EnableInteractionText( string text) // genel 
    {
        interactionText.text = "Pick Up "+ text + " (E)";
        interactionText.gameObject.SetActive(true);
    }


    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);

    }

    void CollectedItems()// old score Update System / new System => Item scriptinde
    {
            //collectedFuelText.text = "Fuel :" + PlayerStatsUiManager.collectedFuel;

            collectedPillText.text = "Pill : " + PlayerStatsUiManager.collectedPill;

            collectedBatteryText.text = PlayerStatsUiManager.collectedBattery.ToString();

    }


}
