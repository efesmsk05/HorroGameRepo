using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using static Unity.Collections.Unicode;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField][Min(1)][Range(0, 5)] float hitRange = 3f;
    [SerializeField] LayerMask dialogueMask;
    private CineMachineManager cineMachineManager;
    private Transform cameraTransform;
    private DialogueRunner dialogueRunner;
    private float defultHorziontalSpeed;
    private float defultVerticalSpeed;

    private RaycastHit hit;

    void Start()
    {
        cineMachineManager = FindObjectOfType<CineMachineManager>();
        cameraTransform = GameObject.Find("Main Camera").transform;
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        defultHorziontalSpeed = cineMachineManager.horziontalSpeed;
        defultVerticalSpeed = cineMachineManager.verticalSpeed;
    }

    void Update()
    {
        StartDialogue();
    }

    void StartDialogue()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            playerController.instance.enabled = true;
            cineMachineManager.horziontalSpeed = defultHorziontalSpeed;
            cineMachineManager.verticalSpeed = defultVerticalSpeed;
            Cursor.lockState = CursorLockMode.Locked; // Ýmleci kilitle
            Cursor.visible = false;                  // Ýmleci görünmez yap
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, hitRange, dialogueMask))
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogueRunner.StartDialogue("Test");
                    playerController.instance.enabled = false;
                    cineMachineManager.horziontalSpeed = 0f;
                    cineMachineManager.verticalSpeed = 0f;

                    Cursor.lockState = CursorLockMode.None; // Ýmleci serbest býrak
                    Cursor.visible = true;                  // Ýmleci görünür yap
                }
            }
        }
    }

    public void PickUpRose()
    {
        dialogueRunner.VariableStorage.SetValue("$hasRose", true);
    }
}