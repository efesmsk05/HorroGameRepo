using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public static Item instance;
    [Header("MissionItems")]
    public UnityEvent missionItemEvent;



    [Header ("Placeable Item Reference")]
    [SerializeField] public GameObject placeableItemPrefab = null;
    [NonSerialized] public Renderer placeableItemRenderer = null;
    [NonSerialized] public Material placeableItemMaterial = null;

    [Header ("Storeable Item Referance")]
    [SerializeField] public GameObject storeableItemPrefab = null;

    //Outline Cache
    [HideInInspector] public Outline outline;

    public ItemTypeData ItemTypeData;

    [SerializeField] public UnityEvent Interaction;
    public bool pickUp;

    public bool pickUpText;
    public string itemName;

    public bool storeableItem;
    public bool useableItem;
    public bool placeableItem;


    private void Awake()
    {
        if (instance == null) 
        instance = this;

        outline = GetComponent<Outline>();

    } 
    void Start()
    {
        if (placeableItemPrefab != null)
        {
            placeableItemRenderer = placeableItemPrefab.GetComponent<Renderer>();
            placeableItemMaterial = placeableItemRenderer.material;
        }

        if (ItemTypeData != null)
        {
            storeableItem = ItemTypeData.storeableItem;
            useableItem = ItemTypeData.useableItem;
            placeableItem = ItemTypeData.placeableItem;


        }


    }

    public void ActiveInteraction()
    {
        Interaction.Invoke();

    }





}
