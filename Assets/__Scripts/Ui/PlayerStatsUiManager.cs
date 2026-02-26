using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUiManager : MonoBehaviour
{
    public static PlayerStatsUiManager instance;

    [Header("PlayerUi -  Stamina")]
    public Image staminaBar;
    public float stamina, maxStamina;
    public int runCost;
    public int jumpCost;
    public bool stamineOver = false;
    private float staminaOverRegenSpeed = .60f;

    //[Header("PlayeUi - GaslightFuel")]
    //[SerializeField] private KeyCode reloadFuelKey = KeyCode.R;
    //public Image fuelbar;
    //float fuel = 100;
    //float maxFuel = 100;    
    //public float fuelCost;
    //public bool fuelOver;
    //public CanvasGroup fuelCanvas;
    //private bool fuelIsReady = false;
    //public  static int collectedFuel = 0;

    [Header ("PlayerUi  - Flashlight")]
    [SerializeField] private GameObject flaslihtGameObject;
    [SerializeField] private KeyCode reloadBatteryKey = KeyCode.R;
    public Image batteryBar;
    float battery = 100;
    float maxBattery = 100;
    public float batteryCost;
    public bool batteryOver;
    public CanvasGroup batteryCanvas;
    private bool batteryIsReady = false;
    public static int collectedBattery = 0;


    [Header("PlayerUi - MentalHealth")]
    [SerializeField] private KeyCode reloadPillKey = KeyCode.P;

    [SerializeField] public float mentalHealth , maxMentalHealth = 100;
    [SerializeField] float mentalHealthCost = 25;
    public Image mentalHealthBar;
    public bool paranoia = false;
    bool mentalOver = false;
    bool mentalFull = false;
    bool pillIsReady = false;
    public static int collectedPill = 0;

    bool regenOn = true;
    float denemeMental;


    [Header("PlayerUi - inGame")]
    public TextMeshProUGUI pickUpText;
    public GameObject flashlight;


    // Basic tools inventory try

    public bool slot1;
    public bool slot2;


    void Start()
    {
        if (instance == null)
            instance = this;

    }

    void FixedUpdate()
    {
        //MentalHealth();
        StaminaSystem();

        // flaslight ve gaslight fuel mantýðý oyunun aþamalarýna göre çaðýrýlcak bu yüzden bir kontrol aþamasý eklenmeli
        //FuelManager();
    }
    private void Update()
    {
        //FlashlightSystem();

    }


    #region Stamina
    private void StaminaSystem()
    {
        if (playerController.instance.isJumping == true && playerController.instance.grounded == false) // Run
        {

            StaminaCostUpgrade(jumpCost);

        }
        else if (playerController.instance.isRunning == true)// jump
        {

            StaminaCostUpgrade(runCost);

        }
        else
        {
            StaminaRegen(runCost);
            return;
        }

    }


    void StaminaCostUpgrade(int value)
    {
        stamina -= value * Time.fixedDeltaTime;
        if(stamina <= 0)// stamina biterse
        {
            stamina = 0;
            stamineOver = true;
            playerController.instance.isRunning = false;
            //playerController.instance.ResetRun();
        }
        staminaBar.fillAmount = stamina / maxStamina;     

    }

    void StaminaRegen(int value)
    {

        if(stamina >= maxStamina)
        {
            stamina = maxStamina;

        }
        else
        {
            stamina += (value * staminaOverRegenSpeed) * Time.fixedDeltaTime;

        }
        if (stamineOver)// stamina full bitince dolana kadar tekrar kullanamýyoruz
        {
            staminaOverRegenSpeed = .30f;// stamina over speed

            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
                stamineOver = false;


            }
        }
        else
        {
            staminaOverRegenSpeed = .60f;

        }


        staminaBar.fillAmount = stamina / maxStamina;



    }

    #endregion


    #region Flaslight Fuel
    void FlashlightSystem()
    {
        if(flaslihtGameObject != null )
        {
            if (FlashlightManager.instance.flaslightOn == true)
            {
                FlashlightCostUpgrade(batteryCost);
                batteryCanvas.alpha += .5f * Time.deltaTime;
                if (batteryCanvas.alpha == 1) { batteryCanvas.alpha = 1; }
            }
            else if (FlashlightManager.instance.flaslightOn == false)
            {
                batteryCanvas.alpha -= 1.5f * Time.deltaTime;
                if (batteryCanvas.alpha == 0) { batteryCanvas.alpha = 0; }
            }

            if (Input.GetKeyDown(reloadBatteryKey) && collectedBattery >= 1 && batteryIsReady == false)
            {
                batteryIsReady = true;
            }
            else if (batteryIsReady == true)
            {

                FlashlightRegen(25);
            }
        }


    }

    void FlashlightCostUpgrade(float value)
    {
        battery -= value * Time.deltaTime;
        if (battery <= 0)
        {
            battery = 0;
            batteryOver = true;
            if (FlashlightManager.instance.hudOn == true)
            {
                FlashlightManager.instance.FlashlightOff();
            }


        }

        batteryBar.fillAmount = battery / maxBattery;
    }

    void FlashlightRegen(float value)
    {
        battery += value * Time.deltaTime;
        if(battery >= maxBattery)
        {
            battery = maxBattery;
            batteryOver = false;
            batteryIsReady = false;
            if(FlashlightManager.instance.hudOn == true)
            {
                FlashlightManager.instance.FlashlightOn();
            }
        }

        batteryBar.fillAmount = battery / maxBattery;
    }

    #endregion

   // #region Gaslight Fuel
    //void FuelManager()
    //{
    //    if(GasLightManager.instance.isGasLightOn == true)
    //    {
    //        GaslightFuelCostUpgrade(fuelCost);
    //        // ui active
    //        fuelCanvas.alpha += .5f * Time.deltaTime;
    //        if(fuelCanvas.alpha == 1) { fuelCanvas.alpha = 1; } 
    //    }
    //    else if (GasLightManager.instance.isGasLightOn == false)
    //    {
    //        //ui close
    //        fuelCanvas.alpha -= 1.5f * Time.deltaTime;
    //        if (fuelCanvas.alpha == 0) { fuelCanvas.alpha = 0; }

    //    }
        
    //    if(!fuelIsReady && fuel <= 100 && collectedFuel >= 1 && Input.GetKey(reloadFuelKey)) //fuel yenileme
    //    {
    //        collectedFuel--; //fuel kullandýðýnda fuel sayýsýný azaltýyor
    //        fuelIsReady = true;
    //    }
    //    if(fuelIsReady)
    //    {
    //        GasLightFuelRegen(50);
    //    }


    //}
    //void GaslightFuelCostUpgrade(float value)
    //{
    //    fuel -= value * Time.fixedDeltaTime;
    //    if(fuel <= 0 )
    //    {
    //        fuel = 0;
    //        fuelOver = true;
    //        GasLightManager.instance.GaslightOff();

    //    }

    //    fuelbar.fillAmount = fuel / maxFuel;

    //}

    //void GasLightFuelRegen(float value)
    //{
    //    fuel += value * Time.fixedDeltaTime;
    //    if(fuel >= maxFuel)
    //    {
    //        fuel = maxFuel;
    //        fuelOver = false;
    //        fuelIsReady = false;
    //        if(GasLightManager.instance.HudOn == true) //HUD açýkken Açýlmasýný saðlýyor
    //        {
    //            GasLightManager.instance.GaslightOn();

    //        }

    //    }
    //    fuelbar.fillAmount = fuel / maxFuel;
    //}

    //#endregion

    #region MentalHealth
    void MentalHealth() // MentalHealt genel çalýþma kontrolü burdan yapýlyýor
    {
        if(PlayerTriggerManager.isInLightArea == false && !pillIsReady) // ýþýk alanlarýna girmeye dayalý sen bunu sonradan düþman için çeþitlendirebilirisin
        {
            MentalHealthCostUpgrade(mentalHealthCost);
        }
        else
        {
            MentalHealthRegen(mentalHealthCost); // deðer 0 alýnýrsa dolmamasýda saðlabilir 
            // sadece hap içilidði zaman regen almasý saðlanabilir ya da belli bir seviye artýp durmasý saðlanabilir
        }

        #region Paranoia Control
        if (mentalHealth < 50  )
        {
            paranoia = true;
        }
        else
        {
            paranoia = false;

        }

        #endregion
        // Mental Health Pill regen

        if (mentalHealth <= 100 && collectedPill >= 1 && !pillIsReady && Input.GetKey(reloadPillKey))
        {
            collectedPill--;
            pillIsReady = true;
            // pillIsReady boolu  pill kullanýlmýyorsa false kullanýldýðý anda true dönüyor 
            //max mental healt duruman geldiðine bool tekrardan false dönüyor

        }

        if (pillIsReady)
        {
            MentalHealthRegen(mentalHealthCost);
        }

    }

    void MentalHealthCostUpgrade(float value)
    {
        mentalHealth -= value * Time.deltaTime;
        if(mentalHealth <= 0) {  
            mentalHealth = 0;
            mentalOver = true; }// mentalHealth Over

        mentalHealthBar.fillAmount = mentalHealth/ maxMentalHealth;

    }


    void MentalHealthRegen(float value)
    {

        mentalHealth += value * Time.fixedDeltaTime;

        if (mentalHealth >= maxMentalHealth)
        {
            pillIsReady = false;
            mentalHealth = maxMentalHealth;
            mentalFull = true;
        }// mentalHealth Full

        mentalHealthBar.fillAmount = mentalHealth / maxMentalHealth;        

    }
    #endregion

    #region ÝnGame Ui
    private void InGameUiController()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    print("slot 1");
        //    flashlight.gameObject.SetActive(false);
        //    slot1 = true;
        //    slot2 = false;
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    print("slot 2");
        //    flashlight.gameObject.SetActive(true);

        //    slot1 = false;
        //    slot2 = true;
        //}

    }

    #endregion


    

}
