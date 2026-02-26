using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManger : MonoBehaviour
{
    public float cameraAcceleration;
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform hand;

    float rotationX;
    float rotationY;



    void Start()
    {
        // Kaydedilen hassasiyet deðerini al
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensX = PlayerPrefs.GetFloat("Sensitivity");
            sensY = PlayerPrefs.GetFloat("Sensitivity");
        }

        Cursor.lockState = CursorLockMode.Locked;// croshairi sabitliyor
        Cursor.visible = false; // cros görünüþünü kapatýyor

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX; //yatay kýsým / forward (ileri)
        rotationX -= mouseY; //dikey kýsým / right 

        rotationX = Mathf.Clamp(rotationX , -90f, 90f); // kameranýn dikey açýsýný sýnýrlýyoruz 90 derece ile 

        // player cam and orientation 

        //hand.rotation = Quaternion.Lerp(hand.rotation , Quaternion.Euler(rotationX, rotationY, 0) , cameraAcceleration * Time.deltaTime); 

        transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(rotationX, rotationY, 0) , cameraAcceleration * Time.deltaTime);
        orientation.rotation = Quaternion.Lerp(orientation.rotation , Quaternion.Euler(0, rotationY, 0) , cameraAcceleration * Time.deltaTime); // movement kýsmýnda kullanacðýnýz karater oryantasyonu

        // Quaternion Lerp --> deðeri yuvarlýyor ve daha bir smooth kamera geçiþi delayý saðlýyor


    }


}
