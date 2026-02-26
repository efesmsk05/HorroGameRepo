using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerManager : MonoBehaviour
{
    public static PlayerTriggerManager instance;

    public static bool isInLightArea = false;
    private void Awake()
    {
        instance = this;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            isInLightArea = true;
        }


        if (other.gameObject.tag == "Scene1Trigger")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                SceneManager.LoadScene("Scene1", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(2);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            isInLightArea = false;
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Light")
        {
            isInLightArea = true;
        }

    }


    


}
