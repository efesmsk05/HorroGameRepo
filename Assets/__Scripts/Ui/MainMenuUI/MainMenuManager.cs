using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    // ----- Slider Class ----
    [System.Serializable]
    public class SliderPref
    {
        public Slider slider;
        public string key;
        public float defaultValue;
    }

    // ------------

    public Camera mainCamera;
    public LayerMask interactableLayer;

    [Header("UI Panels-- Options")]
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public GameObject optionText; // Ayarlar menüsündeki metin paneli


    [Header("Cameras")]
    [SerializeField] GameObject optionsCamera;
    [SerializeField] GameObject startCamera;


    Outline lastOutline = null;
    GameObject lastGO = null;

    public float mainVoýceVolume;

    // Slider Prefs

    [Header("Slider")]
    public List<SliderPref> sliderPrefs;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach (var sp in sliderPrefs)
        {
            // Kaydedilmiþ deðer varsa onu yükle, yoksa default deðeri ata
            if (PlayerPrefs.HasKey(sp.key))
                sp.slider.value = PlayerPrefs.GetFloat(sp.key);
            else
                sp.slider.value = sp.defaultValue;

            // Her slider için ayný fonksiyonu, anahtarý parametre olarak vererek baðla
            sp.slider.onValueChanged.AddListener((sliderValue) => SaveSliderValue(sp.key, sliderValue));

            //var sens = PlayerPrefs.GetFloat("Volume"); // veriye ulaþmak için
            //print(sens);
        }
    }



    void Update()
    {
        // Ekran ve render texture boyutlarýný al
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float rtWidth = mainCamera.targetTexture != null ? mainCamera.targetTexture.width : screenWidth;
        float rtHeight = mainCamera.targetTexture != null ? mainCamera.targetTexture.height : screenHeight;

        // Mouse pozisyonunu render texture'ye orantýla
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x * (rtWidth / screenWidth);
        mousePos.y = mousePos.y * (rtHeight / screenHeight);

        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    // Fare þu an UI üzerindeyse, 3D raycast yapma!
        //    return;
        //}

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green, 1f);

         




        if (Physics.Raycast(ray, out RaycastHit hit, 10000f, interactableLayer))
        {
            Debug.Log("Týklanan obje: " + hit.collider.tag);

            GameObject currentGO = hit.collider.gameObject;
            Outline currentOutline = currentGO.GetComponent<Outline>();

            if (currentOutline != lastOutline)// ayný eþyaymý bakýyorsun
            {
                currentOutline.OutlineWidth = 5f; // Outline geniþliðini ayarla
                lastOutline = currentOutline; // Son bakýlan outline'ý güncelle

            }


        }
        else
        {
            if (lastOutline != null)
            {
                lastOutline.OutlineWidth = 0f;
            }
            lastOutline = null;
        }
    }// raycast

    public void OpenOptionsMenu()
    {
        Debug.Log("Ayarlar menüsü açýlýyor...");
        if (optionsCamera != null)
        {
            optionsCamera.SetActive(true);
        }
        else Debug.LogWarning("Options Camera not assigned!");

    }

    public void CLoseOptionsMenu() // button atanýcak
    {
        print("close");
        Debug.Log("Ayarlar menüsü kapatýlýyor...");


        if (optionsCamera != null)
        {
            optionsPanel.SetActive(false); 
            optionsCamera.SetActive(false);
            optionText.SetActive(true); 
        }
        else Debug.LogWarning("Options Camera not assigned!");

        //if(optionsPanel != null)
        //{
        //    StartCoroutine(optionsPanelClose());    
        //}
    }
    
    public void StartGame()
    {
        StartCoroutine(StartNewGame());
    }
    public IEnumerator StartNewGame()
    {
        startCamera.SetActive(true);
        yield return new WaitForSeconds(2.5f);


        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);




    }

    private void SaveSliderValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        //if (key == "Volume")
        //{
        //    print("Volume kaydedildi: " + value);
        //    mainVoýceVolume = PlayerPrefs.GetFloat("Volume");
        //    // volume ana voluem deðeriyle deðiþtir
        //}
        //if (key == "FxVolume")
        //{
        //    print("FX Volume kaydedildi: " + value);
        //    // FX volume deðeriyle deðiþtir
        //}

    }




    //IEnumerator optionsPanelOpen()
    //{
    //    yield return new WaitForSeconds(3f);
    //    if (optionsPanel != null)
    //    {
    //        optionsPanel.GetComponent<CanvasGroup>().DOFade(1f, 2f).SetEase(Ease.Linear);


    //    }

    //}

    //IEnumerator optionsPanelClose()
    //{
    //    yield return new WaitForSeconds(.2f);
    //    if (optionsPanel != null)
    //    {
    //        optionsPanel.GetComponent<CanvasGroup>().DOFade(0f, .5f).SetEase(Ease.Linear);


    //    }

    //}

    // Save Player Preferences

    //void OnApplicationQuit()
    //{
    //    // Oyuncu tercihlerini kaydet
    //    PlayerPrefs.Save();
    //    Debug.Log("Player preferences saved on application quit.");





}
