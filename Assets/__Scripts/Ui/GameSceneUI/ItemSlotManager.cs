using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotManager : MonoBehaviour
{
    public static ItemSlotManager Instance; // Singleton

    [SerializeField] private Transform handHoldTransform;
    [SerializeField] private GameObject flashlightItem; // Baþlangýç item'i
    [SerializeField] private int maxSlots = 3;

    private GameObject[] itemSlots;
    private int currentSlotIndex = 0;

    [Header("UI & Uyarý")]
    [SerializeField] private Text warningText;
    [SerializeField] private float warningDuration = 2f;
    private float warningTimer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        itemSlots = new GameObject[maxSlots];

        // Flashlight slot 0'a yerleþtirilir
        if (flashlightItem != null)
        {
            itemSlots[0] = flashlightItem;
            flashlightItem.transform.SetParent(handHoldTransform);
            //flashlightItem.transform.localPosition = Vector3.zero;
            flashlightItem.transform.localRotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        HandleSlotSwitch();
        HandleDrop();
        UpdateWarningUI();
    }

    private void HandleSlotSwitch()
    {
        // Tuþlarla geçiþ
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToSlot(2);

        // Scroll ile geçiþ
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SwitchToSlot((currentSlotIndex + 1) % maxSlots);
        if (scroll < 0f) SwitchToSlot((currentSlotIndex - 1 + maxSlots) % maxSlots);
    }

    private void SwitchToSlot(int index)
    {
        if (index == currentSlotIndex) return;

        // Aktif item'ý görünmez yap
        if (itemSlots[currentSlotIndex] != null)
            itemSlots[currentSlotIndex].SetActive(false);

        currentSlotIndex = index;

        // Yeni aktif item'ý göster
        if (itemSlots[currentSlotIndex] != null)
        {
            itemSlots[currentSlotIndex].SetActive(true);
            itemSlots[currentSlotIndex].transform.SetParent(handHoldTransform);
            itemSlots[currentSlotIndex].transform.localPosition = Vector3.zero;
            itemSlots[currentSlotIndex].transform.localRotation = Quaternion.identity;
        }
    }

    private void HandleDrop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject itemToDrop = itemSlots[currentSlotIndex];
            if (itemToDrop != null && itemToDrop != flashlightItem)
            {
                itemToDrop.transform.SetParent(null);
                Rigidbody rb = itemToDrop.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = false;

                itemSlots[currentSlotIndex] = null;
            }
        }
    }

    private void UpdateWarningUI()
    {
        if (warningText == null) return;

        if (warningTimer > 0f)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0f)
                warningText.gameObject.SetActive(false);
        }
    }

    public void TryAddItem(GameObject newItem)
    {
        // Þu anki slota ekle
        if (itemSlots[currentSlotIndex] == null)
        {
            AddItemToSlot(currentSlotIndex, newItem);
            return;
        }

        // Diðer boþ slotlarý kontrol et
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == null)
            {
                AddItemToSlot(i, newItem);
                SwitchToSlot(i);
                return;
            }
        }

        // Hiç boþ slot yoksa uyarý ver
        ShowWarning("Slotlar dolu!");
        Destroy(newItem); // Ýstersen item yok edebilirsin ya da yere býrak
    }

    private void AddItemToSlot(int index, GameObject item)
    {
        itemSlots[index] = item;
        item.transform.SetParent(handHoldTransform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    private void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.gameObject.SetActive(true);
            warningTimer = warningDuration;
        }
    }
}
