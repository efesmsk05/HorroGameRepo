using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimationManager : MonoBehaviour
{
    public GameObject cutScene;
    void Update()
    {
        if(SceneLoadTrigger.Scene1Trigger == true)
        {
            cutScene.gameObject.SetActive(true);
        }
    }
}
