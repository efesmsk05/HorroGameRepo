using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RewievSystem : MonoBehaviour
{ public static RewievSystem instance;

    [SerializeField] public UnityEvent open;
    [SerializeField] public UnityEvent close;
    [SerializeField] private Transform cameraRot;
    [SerializeField] private Transform cameraPos;

    public bool inspectSystemOn = false;    
    public bool showItems;


    public Transform reviewItemPos;
    private void Awake()
    {
        instance = this;
    }
    public void Open()
    {
        transform.position = cameraPos.position;
        transform.rotation = cameraRot.rotation;
        open.Invoke();
        showItems = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        inspectSystemOn = true;



    }

    public void Close()
    {
        close.Invoke();
        showItems = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (reviewItemPos.transform.GetChild(0))
        {
            for (int i = 0; i < reviewItemPos.transform.childCount; i++)
            {
                reviewItemPos.localRotation = Quaternion.identity;

                reviewItemPos.transform.GetChild(i).gameObject.SetActive(false);



            }
            inspectSystemOn = false;

        }

    }



}

