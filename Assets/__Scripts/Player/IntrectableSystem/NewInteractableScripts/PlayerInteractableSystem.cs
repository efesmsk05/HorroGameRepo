using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInteractableSystem : MonoBehaviour
{
    public static PlayerInteractableSystem instance;

    [Header("PreLoad")]
    public MonoBehaviour[] preloadScripts;

    [Header("Keys")]
    [SerializeField] private KeyCode dropItem = KeyCode.Q;
    [SerializeField] private KeyCode InspectItem = KeyCode.V;
    [SerializeField] private KeyCode throwItem = KeyCode.Mouse0;

    [Header("Layers")]
    [SerializeField] LayerMask interactableItems;
    [SerializeField] LayerMask itemsHoldMask;

    [Header("Item Settings")]
    [SerializeField] private float throwForce = 500f;
    [SerializeField][Min(1)][Range(0, 5)] float hitRange = 3f;

    [Header("Transforms")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform handHoldTransform;
    [SerializeField] Transform Inspect;

    [Header("Ui GameObjects")]
    [SerializeField] GameObject pickUpUI;
    [SerializeField] GameObject inHandItem;
    [SerializeField] private GameObject player;

    bool isHoldingItem = false;
    bool isPickedItem = false;
    public bool isInteractable = false;

    private RaycastHit hit;
    private GameObject lastLookedItem = null;

    // --- OPTIMIZE: Outline cache ---
    private Outline lastOutline = null;

    //--OPTIMIZE: Current item cache---
    public GameObject currentGO;
    public Item currentItem;
    private Outline currentOutline;


    public string missionItemName;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //PreLoad();
        HUDController.instance.EnableInteractionText(" ");
        HUDController.instance.DisableInteractionText();
    }

    private void Update()
    {
        ItemInteraction();

        if (isPickedItem)
        {
            if (Input.GetKeyDown(InspectItem))
            {
                GameObject inspectItem = Instantiate(inHandItem, Vector3.zero, quaternion.identity);
                inspectItem.transform.SetParent(Inspect.transform, false);
                RewievSystem.instance.Open();
            }

            if (Input.GetKeyDown(dropItem) && inHandItem.GetComponent<Item>().placeableItem)
            {
                ItemDrop();
                isPickedItem = false;
            }

            if (Input.GetKeyDown(throwItem) && inHandItem.GetComponent<Item>().placeableItem)
            {
                ThrowItem();
                isPickedItem = false;
            }
        }
    }

    void ItemInteraction()
    {
        bool hitSomething = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, hitRange, interactableItems);

        if (hitSomething)
        {
            currentGO = hit.collider.gameObject;
            currentItem = hit.collider.GetComponent<Item>();
            currentOutline = currentItem != null ? currentItem.outline : null;

            // --- OPTIMIZE: Outline yönetimi ---
            if (!isPickedItem && currentOutline != null && !hit.collider.CompareTag("PlaceItem"))
            {
                if (currentGO != lastLookedItem)
                {
                    // Önceki outline'ý kapat
                    if (lastOutline != null)
                        lastOutline.OutlineWidth = 0f;

                    currentOutline.OutlineWidth = 2f;
                    lastOutline = currentOutline;
                }
            }

            // --- OPTIMIZE: UI yönetimi ---
            if (!isPickedItem && currentItem != null)
            {
                if (currentGO != lastLookedItem)
                {
                    HUDController.instance.EnableInteractionText(currentItem.itemName);
                    lastLookedItem = currentGO;
                }
            }
            else if (isPickedItem)
            {
                // Elde item varsa UI ve outline kapatýlýr
                if (lastLookedItem != null)
                {
                    if (lastOutline != null)
                        lastOutline.OutlineWidth = 0f;
                    HUDController.instance.DisableInteractionText();
                    lastLookedItem = null;
                    lastOutline = null;
                }
            }

            // Item alma iþlemi
            if (currentItem != null && Input.GetKeyDown(KeyCode.E) && !isPickedItem && currentItem.ItemTypeData.eventItemType == ItemTypes.EventItemType.none)
            {
                inHandItem = currentGO;
                isPickedItem = true;
                ItemPick();
            }


            // Item Place
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("PlaceItem") && inHandItem != null)
            {
                print("efe");
                var inHandItemComponent = inHandItem.GetComponent<Item>();
                if (inHandItemComponent != null && inHandItemComponent.placeableItemPrefab != null)
                {
                    Transform placeItemRef = inHandItemComponent.placeableItemPrefab.transform;
                    inHandItem.transform.position = placeItemRef.position;
                    inHandItem.transform.rotation = placeItemRef.rotation;
                    inHandItemComponent.placeableItemPrefab.SetActive(false);
                    inHandItem.transform.SetParent(null);
                    inHandItem.layer = LayerMask.NameToLayer("Default");// tekrar etkileþime girmemesi için layer deðiþtirildi
                    inHandItem = null;
                    isPickedItem = false;
                }
            }

            ////// Event item kontrolü
            if (Input.GetKeyDown(KeyCode.E) && currentItem.ItemTypeData != null && currentItem.ItemTypeData.eventItemType != ItemTypes.EventItemType.none)
            {
                // item kontrolü 
                print("kontrol saðlandý");
                EventItem(currentItem.ItemTypeData, currentGO);
            }


            // CutScene tetikleme
            //if (hit.collider.CompareTag("CutScene"))
            //{
            //    if (!isInteractable && Input.GetKeyDown(KeyCode.E))
            //    {
            //        SceneLoadTrigger.instance.ScenePass01();
            //        isInteractable = true;
            //    }
            //}
        }
        else
        {
            // Hiçbir itema bakýlmýyorsa UI ve outline kapatýlýr
            if (lastLookedItem != null)
            {
                if (lastOutline != null)
                    lastOutline.OutlineWidth = 0f;
                HUDController.instance.DisableInteractionText();
                lastLookedItem = null;
                lastOutline = null;
            }
        }
    }


    void ItemPick()
    {
        if (inHandItem != null)
        {
            Item item = inHandItem.GetComponent<Item>();
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();

            if (item.placeableItem)// eþyayý eline al
            {
                inHandItem.layer = LayerMask.NameToLayer("Interactable");


                var outline = inHandItem.GetComponent<Outline>();
                if (outline != null)
                    outline.OutlineWidth = 0f;


                //inHandItem.transform.position = Vector3.zero;
                //inHandItem.transform.rotation = Quaternion.identity;

                inHandItem.transform.SetParent(handHoldTransform);
                inHandItem.gameObject.transform.localPosition = Vector3.zero;
                inHandItem.gameObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);




                if (rb != null)
                    rb.isKinematic = true;

                Physics.IgnoreCollision(inHandItem.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

                // Placeable item shader referansý
                if (item.placeableItem && item.placeableItemPrefab != null)
                {
                    Outline outlineRef = item.placeableItemPrefab.GetComponent<Outline>();
                    if (outlineRef != null)
                        outlineRef.OutlineWidth = 2f;
                    item.placeableItemPrefab.SetActive(true);
                }


                //Inventy güncellemesi
                //BasicInventroy.instance.InventoryUpdate(item.itemSprite , item.itemName , inHandItem);

            }

            if (item.useableItem)// eþyayý yok et depola
            {
                InventroyUiManager.Instance.UpdateResourcesUi(item.ItemTypeData.itemName);

                isPickedItem = false;
                Destroy(inHandItem);
                inHandItem = null;
                HUDController.instance.DisableInteractionText();
            }

            if(item.storeableItem)
            {
                inHandItem.layer = LayerMask.NameToLayer("Inspect");
                isPickedItem = false;

                InventoryManager.Instance.InventoryRegister(new Inventroy
                {
                    itemID = item.ItemTypeData.itemName,
                    itemDescription = item.ItemTypeData.description,
                    itemReferance = inHandItem,

                });

                if (rb != null)
                {
                    inHandItem.transform.position = Vector3.zero;
                    inHandItem.transform.rotation = Quaternion.identity;
                    rb.isKinematic = true;
                    inHandItem.transform.SetParent(Inspect, false);

                    inHandItem.gameObject.SetActive(false);
                    inHandItem = null;
                }


            }

        }
    }



    void ItemDrop()
    {
        if (inHandItem != null)
        {
            inHandItem.layer = LayerMask.NameToLayer("Interactable");


            var itemComponent = inHandItem.GetComponent<Item>();
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            inHandItem.transform.SetParent(null);



            if (rb != null)
            {
                rb.isKinematic = false;
                inHandItem = null;
            }

            if (itemComponent != null && itemComponent.placeableItemPrefab != null && itemComponent.placeableItem)
            {
                itemComponent.placeableItemPrefab.SetActive(false);
            }

        }
    }

    void ThrowItem()
    {
        if (inHandItem != null)
        {
            inHandItem.layer = LayerMask.NameToLayer("Interactable");

            var itemComponent = inHandItem.GetComponent<Item>();
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(inHandItem.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            inHandItem.transform.SetParent(null);
            isHoldingItem = false;

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(cameraTransform.forward * throwForce);
            }


            if (itemComponent != null && itemComponent.placeableItemPrefab != null && itemComponent.placeableItem)
            {
                itemComponent.placeableItemPrefab.SetActive(false);
            }

            inHandItem = null;
        }
    }

    void PreLoad()
    {
        foreach (var script in preloadScripts)
        {
            var dummy = new GameObject("ScriptDummy");
            dummy.AddComponent(script.GetType());
            Destroy(dummy);
        }

        HUDController.instance.EnableInteractionText(" ");
        HUDController.instance.DisableInteractionText();
    }

    void EventItem(ItemTypeData data , GameObject item)
    {
        switch(data.eventItemType)
        {
            case ItemTypes.EventItemType.makeSound:

                // Burada sesi çalacak kodu yazabilirsiniz
                print("make sound");
                if (data.soundClip  !=null)
                {
                    LevelSoundManager.instance.PlayFx(data.soundClip);
                }
                break;

            case ItemTypes.EventItemType.completeTask:
                QuestUiManager.Instance.CompleteQuestUi(data.questID);
                QuestManager.Instance.CompletedQuest(data.questID);
                item.gameObject.SetActive(false);

                break;


            default:
                // Hiçbir iþlem yapma, geçerli bir event item deðilse
                break;


        }


    }

    private void OnBeforeTransformParentChanged()
    {
        if (inHandItem != null)
        {
            var itemComponent = inHandItem.GetComponent<Item>();
            if (currentItem != null && itemComponent.placeableItemPrefab != null)
            {
                currentItem.placeableItemPrefab.SetActive(false);
            }
        }
    }


    
}