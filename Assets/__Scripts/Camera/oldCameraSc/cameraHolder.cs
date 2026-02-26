using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHolder : MonoBehaviour
{
    public Transform cameraPos; 

    public void Update()
    {
        transform.position = cameraPos.position;

        
        

    }


}
