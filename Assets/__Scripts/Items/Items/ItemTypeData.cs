using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Item", menuName = "Creat Interactable Item")]
public class ItemTypeData : ScriptableObject
{
    public string itemName = "Item";

    public bool storeableItem;
    public bool useableItem;
    public bool placeableItem;

    [Header("Eðer Obje PlaceAbleItem ise")]
    public GameObject placeShaderObject;

    [Header("EventItem")]
    public ItemTypes.EventItemType eventItemType = ItemTypes.EventItemType.none;
    public AudioClip soundClip; // For sound events
    public string questID; // For quest-related items

    [Header("CollectableItems")]
    [SerializeField] public string description = "Description of the item";
    public Sprite itemSprite;

    


}
