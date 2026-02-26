using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Inspector'dan baðlayacaðýn Slider
    public float defaultSensitivity = 10f; // Baþlangýç hassasiyeti

    void Start()
    {
        // Daha önce kaydedilmiþ hassasiyet deðeri varsa, onu kullan
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            sensitivitySlider.value = defaultSensitivity;
        }

        // Slider her deðiþtiðinde ApplySensitivity çalýþsýn
        sensitivitySlider.onValueChanged.AddListener(SaveSensitivity);
    }

    // Slider deðeri deðiþtiðinde bu fonksiyon çalýþacak ve hassasiyeti kaydedecek
    private void SaveSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }

}
