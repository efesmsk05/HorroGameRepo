using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FlashlightManager : MonoBehaviour
{
    public static FlashlightManager instance;

    [Header ("Lights")]
    [SerializeField] private Light flashlight;
    [SerializeField] GameObject UvControl;

    // bu baþlýðý daha kalýn veya daha farklý stilde yaz
    [Header("Lights Values--Normal Mode")] 
    [SerializeField] private float flashlightIntensity = 60f;
    [SerializeField] private float flashlightRange = 20f;
    [SerializeField] private float flashlightSpotAngle = 80f;

    [Header("Lights Values--Long Mode")]
    [SerializeField] private float flashlightIntensityLong = 70f;   
    [SerializeField] private float flashlightRangeLong = 40f;
    [SerializeField] private float flashlightSpotAngleLong = 55f;

    [Header("Keys")]
    [SerializeField] KeyCode flaslightKey = KeyCode.F;
    [SerializeField] KeyCode uvLightKey = KeyCode.Mouse1;

    [HideInInspector]
    public bool uvMod;
    [HideInInspector]
    public bool flaslightOn = false;
    [HideInInspector]
    public bool hudOn = false;
    [HideInInspector]
    public bool flashlightMode = false;



    private void Awake()
    {
        if (instance == null)
            instance = this;    

    }

    void Start()
    {
    flaslightOn = false;

    flashlight.enabled = false;
    }


    void Update()
    {

        FlaslightManager();
        FlashlightMode();

        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);

        // item layerýna deyen raycast yapýcaz ve bu raycas deyince flashlightýn intensitsini Lerp kullanarak kýsýcaz raycasttan çýkýcada normala lerp ile döndürücez



    }

    void FlaslightManager()
    {
       if(Input.GetKeyDown(flaslightKey) && flaslightOn == false)
       {
            if (PlayerStatsUiManager.instance.batteryOver == true)
            {
                FlashlightOff();
            }
            else
            {
                FlashlightOn();
            }

            flaslightOn = true;
            hudOn = true;

        }
        else if(Input.GetKeyDown(flaslightKey) && flaslightOn)
       {

            FlashlightOff();
            flaslightOn = false;
            hudOn= false;
       }

    }

    public void FlashlightOn()
    {
        flashlight.enabled = true;
    }

    public void FlashlightOff()
    {
        flashlight.enabled = false;
    }

    void FlashlightMode()
    {
        #region Flashlight Long mode
        if (Input.GetKey(KeyCode.Mouse0) && flashlightMode == false)
        {
            PlayerStatsUiManager.instance.batteryCost += 5f;
            flashlight.intensity = flashlightIntensityLong;
            flashlight.range = flashlightRangeLong;
            flashlight.spotAngle = flashlightSpotAngleLong;
            //flashlight.innerSpotAngle = 80;

            flashlightMode = true;


        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && flashlightMode == true) // normal mode
        {
            PlayerStatsUiManager.instance.batteryCost -= 5f;

            flashlight.intensity = flashlightIntensity;
            flashlight.range = flashlightRange;
            //flashlight.innerSpotAngle = 80;
            flashlight.spotAngle = flashlightSpotAngle;
            flashlightMode = false;

        }
        #endregion

        #region UVmode
        if (flaslightOn == true && Input.GetKey(uvLightKey))
        {

            UvControl.SetActive(true);
            flashlight.enabled = false;
            uvMod = true;


        }
        else if(Input.GetKeyUp(uvLightKey) && flaslightOn)
        {

            UvControl.SetActive(false);
            flashlight.enabled = true;
            uvMod = false;



        }
        #endregion

    }

}
