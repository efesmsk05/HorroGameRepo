using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInventroy : MonoBehaviour
{
    public static BasicInventroy instance;
     Sprite ýtemSrite;


    [SerializeField] private Image[] uiHotBarSlots;
    //Cep Itmes
    public Image cep0;
    Image cep1;

    GameObject slotItem;



    private void Awake()
    {
        instance = this;
    }
    public void InventoryUpdate(Sprite itemSprite , string itemName , GameObject handItem , bool active)
    {

        slotItem = handItem;
        print(itemSprite + " " + itemName);

        cep0.sprite = itemSprite;
    }

    public void InventoryDeleteUpdate()
    {
        if (cep0 != null)
        {
            cep0.sprite = null;
        }
    }

}
