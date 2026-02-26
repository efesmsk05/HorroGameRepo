using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    public static SceneLoadTrigger instance;

     [SerializeField] SceneField[] scenesToLoad;
     //[SerializeField] SceneField[] scenesToUnLoad;
    [SerializeField] GameObject[] scenesToUnLoad;
    public static bool Scene1Trigger = false;

    [SerializeField] private GameObject cameraSens;
    public GameObject cutScene;

    

    private void Awake()
    {
        instance = this;
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.gameObject.tag == "Player")
    //    //{



    //    //}
    //}

    public void ScenePass01(string sceneName)
    {

        if (Scene1Trigger == false)
        {
            ////camera sens control

            cameraSens.GetComponent<CineMachineManager>().cutSceneSens = 0;
            StartCoroutine(CutScene());
            //cutScene.SetActive(true);
            Scene1Trigger = true;
            LoadScenes();
            SceneManager.UnloadSceneAsync(sceneName);
            PlayerInteractableSystem.instance.isInteractable = false;

        }
    }

    private void LoadManager()
    {
        LoadScenes();
        SceneManager.UnloadSceneAsync("Scene0");
    }
    private void LoadScenes()
    {
        for(int i = 0; i < scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;

            for (int x = 0; x < SceneManager.sceneCount; x++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(x);
                if (loadedScene.name == scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(scenesToLoad[i] , LoadSceneMode.Additive);

            }
        }
    }

    private void UnLoadScenes()
    {
        //for (int i = 0; i < scenesToUnLoad.Length; i++)
        //{
        //    for (int x = 0;  x < SceneManager.sceneCount;  x++)
        //    {
        //        Scene loadedScene = SceneManager.GetSceneAt(x);
        //        if (loadedScene.name == scenesToUnLoad[i].SceneName)
        //        {
        //            SceneManager.UnloadSceneAsync(scenesToUnLoad[i]);
        //        }

        //    }

        //}

        for ( int x = 0; x < scenesToUnLoad.Length; x++)
        {
            for (int i = 0; x < SceneManager.sceneCount; i++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name == scenesToUnLoad[x].ToString())
                {
                    SceneManager.UnloadSceneAsync(scenesToUnLoad[x].ToString());
                }

            }
        }
    }

    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(3f);
        cameraSens.GetComponent<CineMachineManager>().cutSceneSens = 1;

        //cutScene.SetActive(false);



    }
}
