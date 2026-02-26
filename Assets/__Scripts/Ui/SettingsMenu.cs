using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    // ----- Slider Class ----
    [System.Serializable]
    public class SliderPref
    {
        public Slider slider;
        public string key;
        public float defaultValue;
    }

    // ------------

    public List<SliderPref> sliderPrefss;
    public GameObject settingsPanel;
    public GameObject menu;


    private void Start()
    {
        foreach (var sp in sliderPrefss)
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
    public void OpenSettings()
    {
        menu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        menu.SetActive(true);
        settingsPanel.SetActive(false);
    }

    private void SaveSliderValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        print("save values");
        if (key == "Volume" || key == "FxVolume")
        {
            print("saved");
            LevelSoundManager.instance.SaveVolumeSettings(key);

        }


    }
}
