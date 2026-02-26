using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject[] maskObjects;

    void Start()
    {
        for (int i = 0; i < maskObjects.Length; i++)
        {
            maskObjects[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }


}
