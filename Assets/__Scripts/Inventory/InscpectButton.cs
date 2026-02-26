using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InscpectButton : MonoBehaviour
{
    
    public void InspectItem()
    {
        if(InventoryManager.Instance.Inventroys.TryGetValue(this.transform.parent.name , out var ýnventroy))
        {
            print($"Inspecting item: {ýnventroy.itemID}");
            ýnventroy.itemReferance.SetActive(true);
            InventroyUiManager.Instance.CloseNotebook();
            RewievSystem.instance.Open();

        }
    }
}
