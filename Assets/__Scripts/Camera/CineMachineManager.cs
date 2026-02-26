using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineManager : CinemachineExtension
{

    public Vector3 startingRotation;
    public Transform cameraTransform;
    public float horziontalSpeed = 5f;
    public float verticalSpeed = 5f;
    public float cameraAcceleration = 25f;

    public float sensX = 50f;
    public float sensY = 50f;
    public float cutSceneSens = 1f;

    public float minAngleY = -70f;
    public float maxAngleY = 80f;

    public float maxAngleX = 45f; // maximum angle for camera rotation
    public float minAngleX = -45f; // minimum angle for camera rotation

    public bool isCutScene = false; // flag to check if it's a cutscene


    private void Start()
    {
        startingRotation = new Vector3(180f, 0f, 0f);
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("sens"))
        {
            sensX = PlayerPrefs.GetFloat("sens");
            sensY = PlayerPrefs.GetFloat("sens");
        }
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        //throw new System.NotImplementedException();
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX* cutSceneSens;
                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY * cutSceneSens;
                Vector2 deltaInput = new Vector2(mouseX , mouseY);

                startingRotation.x += deltaInput.x * verticalSpeed;
                startingRotation.y += deltaInput.y * horziontalSpeed;
                startingRotation.y = Mathf.Clamp(startingRotation.y ,minAngleY , maxAngleY);

                if(isCutScene == true) 
                { 
                    startingRotation.x = Mathf.Clamp(startingRotation.x, minAngleX, maxAngleX); // Clamp the x rotation to the defined limits
                }
     
                state.RawOrientation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f), cameraAcceleration * Time.deltaTime);



            }
        }

    }
}
