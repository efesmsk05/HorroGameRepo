using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;

    [Header ("Walk")]
    [Range(.001f,  .01f)]
    [SerializeField] private float amount =.002f;
    [Range(1f, 30f)]
    [SerializeField] private float frequnecy = 10f;
    [Range(10f, 100f)]

    [SerializeField] private float smooth;


    [Header ("Run")]
    [Range(.001f, .01f)]
    [SerializeField] private float runAmount = .005f;
    [Range(1f, 30f)]
    [SerializeField] private float runFrequnecy = 15f;
    [Range(10f, 100f)]
    [SerializeField] private float runSmooth;

    [Header("FlashLight")]
    public GameObject flashLight;
    Vector3 startPos;
    Vector3 flashlightStartPos;
    [Range (10f, 500f)]
    public float flashlightMoveSpeed = 100f;

    void Start()
    {
        startPos = transform.localPosition;
        flashlightStartPos = flashLight.transform.localPosition;
        
    }

    void Update()
    {
        CheckForHeadBobTrigger();
        StopHeadBob();
    }

    private void CheckForHeadBobTrigger()
    {
        if(playerController.instance.isWalking == true)
        {
            WalkingHeadBob();
            RunningFOVin();

        }
        else if(playerController.instance.isRunning == true)
        {
            RunningHeadBob();
            RunnigFOVout();

        }

    }

    private Vector3 WalkingHeadBob()
    {
        Vector3 lightPos = Vector3.zero;
        Vector3 pos = Vector3.zero;

        lightPos.x += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequnecy) * amount * flashlightMoveSpeed, smooth * Time.deltaTime);
        lightPos.y += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequnecy / 2) * amount * flashlightMoveSpeed, smooth * Time.deltaTime);


        pos.y += Mathf.Lerp(pos.y , Mathf.Sin(Time.time * frequnecy)* amount*1.4f , smooth*Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x  , Mathf.Cos(Time.time * frequnecy /2) * amount * 1.6f,smooth*Time.deltaTime);


        flashLight.transform.localPosition += lightPos;

        transform.localPosition += pos;

        return pos;
    }

    private Vector3 RunningHeadBob()
    {

        Vector3 pos = Vector3.zero;

        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * runFrequnecy) * runAmount * 1.4f, runSmooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * runFrequnecy / 2) * runAmount * 1.6f, runSmooth * Time.deltaTime);

        transform.localPosition += pos;

        return pos;
    }

    private void StopHeadBob()
    {
        if(transform.localPosition == startPos)
        {
            return;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 1 * Time.deltaTime);
        flashLight.transform.localPosition = Vector3.Lerp(flashLight.transform.localPosition, flashlightStartPos, 1 * Time.deltaTime);

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
