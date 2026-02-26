using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Test : MonoBehaviour
{
    [SerializeField] float lerpSpeed = 3;
    [SerializeField] float lookDistance = 3;

    private Transform playerTransform;
    private DialogueRunner dialogueRunner;
    private GameObject headTrigger;
    private Vector3 defaultTriggerPos;

    void Start()
    {
        playerTransform = playerController.instance.transform;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        headTrigger = GameObject.Find("HeadTrigger");
        defaultTriggerPos = headTrigger.transform.position;
    }


    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        if (Vector3.Distance(this.transform.position, playerTransform.position) <= lookDistance)
        {
            headTrigger.transform.position = Vector3.Lerp(headTrigger.transform.position, playerTransform.position + new Vector3(0, 1, 0), lerpSpeed * Time.deltaTime);
        }
        else
        {
            headTrigger.transform.position = Vector3.Lerp(headTrigger.transform.position, defaultTriggerPos, lerpSpeed * Time.deltaTime);
        }
    }
}
