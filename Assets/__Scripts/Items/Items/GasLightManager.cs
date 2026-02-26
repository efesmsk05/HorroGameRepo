using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasLightManager : MonoBehaviour
{
    public static GasLightManager instance;


    [Header ("Gaslight Settings")]
    public SphereCollider lightArea;
    public Transform cameraRotation;
    public Transform gasLightGameObject;
    public Light gasLight;
    public KeyCode gasLightKey = KeyCode.F;
    public Image gasLightImg;
    public bool isGasLightOn = false;
    public bool HudOn = false;  
    private bool lightVibratation = false;
    float vib = .5f;

    private Vector3 atackmodePos;        
    

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
    void Start()
    {
        if(isGasLightOn == false)
        {
            gasLight.GetComponent<Light>().enabled = false;
        }
        StartCoroutine(GaslightVibration());


    }

    void Update()
    {
  
        //gasLightController();
        //GaslightVibration();

        this.gameObject.transform.rotation = cameraRotation.rotation;
    }

    //private void gasLightController()
    //{
    //    if(Input.GetKeyDown(gasLightKey) && !isGasLightOn)
    //    {
    //        lightVibratation = true;
    //        StartCoroutine(GaslightVibration());
    //        HudOn = true;

    //        if(PlayerStatsUiManager.instance.fuelOver == false)
    //        {
    //            GaslightOn();

    //        }
    //        else
    //        {
    //            GaslightOff();

    //        }
    //        gasLightImg.GetComponent<Image>().enabled = true;
    //        isGasLightOn = true;

    //    }
    //    else if(Input.GetKeyDown(gasLightKey) && isGasLightOn)
    //    {
    //        lightVibratation = false;


    //        HudOn = false;

    //        GaslightOff();
    //        gasLightImg.GetComponent<Image>().enabled = false;

    //        isGasLightOn = false;

    //    }

    //}


    public void GaslightOn()
    {
        PlayerTriggerManager.isInLightArea = true;// ýþýk açýkken mental healt kontrolü 
        gasLight.GetComponent<Light>().enabled = true;

    }

    public void GaslightOff()
    {
        PlayerTriggerManager.isInLightArea = false;// ýþýk kapalýyken mental healt kontrolü 

        gasLight.GetComponent<Light>().enabled = false;

    }


    IEnumerator GaslightVibration()
    {

        if(lightVibratation == true)
        {
            float maxVib = 4.5f;
            //float minVib = 3f;


            while (vib <= maxVib)
            {
                vib += .1f;
                gasLight.GetComponent<Light>().intensity = vib;

                yield return new WaitForSeconds(.1f);

            }


            yield return new WaitForSeconds(.1f);
            lightVibratation = false;
            vib = .5f;

        }



    }

}
