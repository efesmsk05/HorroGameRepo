using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    [SerializeField] Transform playerRot;
    [SerializeField]float rootSpeed;
    void Start()
    {
        this.transform.rotation = playerRot.rotation;
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, playerRot.rotation, rootSpeed * Time.deltaTime);
        //this.transform.rotation =   playerRot.rotation;
    }
}
