using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventroyUiManager : MonoBehaviour
{
    public static InventroyUiManager Instance { get; private set; }

    [Header ("InventoryStorableSlotsReferance")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private GameObject referancObject;
    [SerializeField] private Transform referanceTransform;

    [Header ("InventoryUseableSlotsReferance")]

    [SerializeField] private GameObject pillSLot;
    [SerializeField] private GameObject pillImg;
    [SerializeField] private GameObject batterySlot;
    [SerializeField] private GameObject batteryImg;


    [Header ("Events")]
    public UnityEvent Open;
    public UnityEvent Close;
    public UnityEvent OpenNotes;
    public UnityEvent OpenQuests;
    public UnityEvent OpenInventory;
    public UnityEvent CloseInventory;

    [SerializeField] private Transform cameraRot;
    [SerializeField] private Transform cameraPos;

    private bool isInventoryOpen = false;

    private bool firstPillIsTaked = false;
    private  bool firstBatteryIsTaked = false;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&& isInventoryOpen== false)
        {
            if (Open != null)
            {
                OpenInventoryUi();
                isInventoryOpen = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen == false)
        {
            if(OpenQuests != null)
            {
                OpenQuestsUi();
                isInventoryOpen = true;

            }
        }


        if (Input.GetKeyDown(KeyCode.P) && isInventoryOpen == false)
        {
            if (OpenNotes != null)
            {
                OpenNotesUi();
                isInventoryOpen = true;

            }
        }


        if (Input.GetKeyDown(KeyCode.Escape)&& isInventoryOpen)
        {
            if (Close != null)
            {
                CloseNotebook();
                isInventoryOpen = false;

            }
        }
    }

    public void OpenNotebook()
    {
        transform.position = cameraPos.position;
        transform.rotation = cameraRot.rotation;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Open?.Invoke();


    }

    public void CloseNotebook()
    {
        Close?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenInventoryUi()
    {
        OpenNotebook();
        OpenInventory?.Invoke();
    }

    public void CloseInventoryUi()
    {
        CloseNotebook();
        CloseInventory?.Invoke();
    }
    public void OpenNotesUi()
    {
        OpenNotebook();
        
        OpenNotes?.Invoke();

    }

    public void OpenQuestsUi()
    {
        OpenNotebook();

        OpenQuests?.Invoke();
    }



    public void UpdateInventory(string itemName , string itemDesc , Sprite itemImage)
    {

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDesc;
        itemIconImage.sprite = itemImage;
        GameObject newSlot = Instantiate(referancObject, referanceTransform);
        newSlot.gameObject.name = itemName;


        newSlot.SetActive(true);
    }

    public void UpdateResourcesUi(string itemID)
    {
        if(itemID == "Pill")
        {
            PlayerStatsUiManager.collectedPill += 1;
            pillSLot.SetActive(true);
            GameObject newPill = Instantiate(pillImg, pillSLot.transform);
            newPill.SetActive(true);

        }


        if (itemID == "Battery")
        {
            PlayerStatsUiManager.collectedBattery += 1;
            batterySlot.SetActive(true);
            GameObject newBattery = Instantiate(batteryImg, batterySlot.transform);
            newBattery.SetActive(true);

        }

    }

    



}

