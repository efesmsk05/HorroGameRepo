using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Review : MonoBehaviour
{


    [SerializeField] private Transform objectToInspect;
    [SerializeField] private float rotSpeed = 100f;

    [SerializeField]  [Range (0,20)]float zoomOutRange = 2f;
    [SerializeField] [Range(0, 20)] float zoomInRange = 2f;

    private Vector3 previousMousePos;

    void Update()
    {
        Inspect();

        if (Input.GetKeyDown(KeyCode.Escape)&& RewievSystem.instance.showItems == true)
        {
            RewievSystem.instance.Close();
            InventroyUiManager.Instance.OpenInventoryUi();




        }
    }
    void Inspect()
    {
        #region Rotate 
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePos = Input.mousePosition - previousMousePos;

            float rotX = deltaMousePos.y * Time.deltaTime * rotSpeed;
            float rotY = deltaMousePos.x * Time.deltaTime * rotSpeed;

            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);

            objectToInspect.rotation = rot * objectToInspect.rotation;

            previousMousePos = Input.mousePosition;


        }
        #endregion

        #region Zoom in out 

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll > 0f)
        {
            //print("scrol up");
            // zoom
            

            Vector3 zoomPos = new Vector3(0f, 0f, Mathf.Clamp(objectToInspect.localPosition.z + zoomOutRange * Time.deltaTime , 0.65f, 2f));

            objectToInspect.localPosition = zoomPos;




        }
        else if (scroll < 0f)
        {


            Vector3 zoomPos = new Vector3(0f, 0f,Mathf.Clamp(objectToInspect.localPosition.z - zoomInRange * Time.deltaTime , 0.65f, 2f));

            objectToInspect.localPosition = zoomPos;


        }
        #endregion

    }
}
