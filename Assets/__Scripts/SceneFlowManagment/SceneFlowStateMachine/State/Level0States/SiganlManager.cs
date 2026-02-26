using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiganlManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private GameObject player;
    public void level0_GameStartSignal()
    {
        Debug.Log("Level 0 Game Start Signal Triggered");

        player.gameObject.transform.position = new Vector3(-107.460846f, 6.44500017f, -45.0019989f);
        // Burada Level 0 için gerekli baþlangýç sinyallerini tetikleyebilirsiniz
        // Örneðin, karakterin masada oturduðunu belirten bir olay tetikleyebilirsiniz
    }
}
