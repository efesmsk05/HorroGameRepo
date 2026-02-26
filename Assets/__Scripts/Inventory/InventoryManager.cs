using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    //

    public Dictionary<string, Inventroy> Inventroys = new ();
    private void Awake()
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

    private void Start()
    {
    }

    public void InventoryRegister(Inventroy inventroy)
    {
        if(Inventroys != null && !Inventroys.ContainsKey(inventroy.itemID))
        {
            Inventroys.Add(inventroy.itemID, inventroy);
            inventroy.isEquipped = true;
            InventroyUiManager.Instance.UpdateInventory(inventroy.itemID , inventroy.itemDescription , inventroy.itemIcon);
            print($"Item {inventroy.itemID} item eklendi");

        }
        else
        {
            print($"Item {inventroy.itemID} already exists in the inventory.");
        }

    }

    public bool CheckInventoryItem(string itemID)// quest bool controler
    {
        return Inventroys.TryGetValue(itemID, out var item) && item.isEquipped;
    }

}
