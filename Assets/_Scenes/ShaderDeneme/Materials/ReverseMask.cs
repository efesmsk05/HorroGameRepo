using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseMask : MonoBehaviour
{
    public GameObject[] objects;
    public int renderQueueNum;
    void Start()
    {
        
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<MeshRenderer>().material.renderQueue = renderQueueNum;

        }
    }


}
