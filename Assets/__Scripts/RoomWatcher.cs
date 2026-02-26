using UnityEngine;
using System.Collections.Generic;

public class RoomWatcher : MonoBehaviour
{
    public Camera playerCamera;
    public Collider roomCollider;
    public List<GameObject> objectsInRoom = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var obj in objectsInRoom)
            {
                TeleportableObject teleportScript = obj.GetComponent<TeleportableObject>();
                if (teleportScript == null)
                {
                    teleportScript = obj.AddComponent<TeleportableObject>();
                }

                teleportScript.playerCamera = playerCamera;
                teleportScript.roomBounds = roomCollider;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var obj in objectsInRoom)
            {
                var script = obj.GetComponent<TeleportableObject>();
                if (script != null)
                    Destroy(script);
            }
        }
    }
}
