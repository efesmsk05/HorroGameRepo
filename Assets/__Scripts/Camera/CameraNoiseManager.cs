using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CameraNoiseManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    public UnityEvent[] shock;    
    bool heartBeatShake= false;
    bool runningShake  =false;
    bool walkingShake = false;
    private void Update()
    {
        #region Walk Camera
        if(playerController.instance.idle == false && playerController.instance.isRunning == false && walkingShake == false)
        {
            if(playerController.instance.walkingTimerControl == true && playerController.instance.isWalking == true) // yürümeye baþladýktan 1 saniye sonra efektin gerçekleþme kontrolünü yapýyor
            {
                InvokeRepeating("WalkNoise", 0f, 1f);
                walkingShake = true;
            }

        }
        else if (playerController.instance.idle == true || playerController.instance.isRunning == true || playerController.instance.isWalking == false)
        {
            CancelInvoke("WalkNoise");
            walkingShake = false;
        }

        #endregion

        #region Run Camera
        if (playerController.instance.isRunning == true && runningShake == false)
        {
            runningShake = true;

            InvokeRepeating("RunNoise", .01f, .5f);
            //RunnigFOVout();


        }
        else if (playerController.instance.isRunning == false)
        {
            CancelInvoke("RunNoise");
            RunningFOVin();
            runningShake = false;
        }

        if(playerController.instance.isRunning == true)
        {
            RunnigFOVout();

        }
        #endregion

        #region HeartBeat
        //if (PlayerStatsUiManager.instance.mentalHealth < 75 && !heartBeatShake)
        //{
        //    InvokeRepeating("Paranoia", 1f, 1f);
        //    heartBeatShake = true;
        //}
        //else if (PlayerStatsUiManager.instance.mentalHealth > 75)
        //{
        //    CancelInvoke("Paranoia");
        //    heartBeatShake = false;

        //}
        #endregion
    }

    private void Paranoia()// Repeating özelliðini kullanmka için ayrý bir fnc açýldý
    {
        shock[1].Invoke();
        
    }

    void RunNoise()
    {
        shock[0].Invoke();
    }

    void WalkNoise()
    {
        shock[2].Invoke();

    }

    private void RunningFOVin() // fov 
    {
        if (mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView >= 66)
        {
            mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView -= 7f * Time.deltaTime;
            if (mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView <= 66)
            {
                mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 66;

            }
        }
    }

    private void RunnigFOVout()// fov 
    {
        if (mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView <= 73)
        {
            mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView += 15f * Time.deltaTime;

        }

    }



}
