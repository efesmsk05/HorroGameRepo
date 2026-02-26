using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject playerHUD;
    public GameObject settingsPanel;


    private bool isPaused = false;
    private bool isNotebookActive = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ayarlar açýksa ESC ile geri dön
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
                pauseMenuPanel.SetActive(true);
            }
            // Oyun duraklatýldýysa ESC ile devam et
            else if (isPaused)
            {
                ResumeGame();
            }
            // Oyun aktifse ESC ile duraklat
            else if (!isPaused )
            {
                PauseGame();
            }

  
            
        }
        

    }

    public void ResumeGame()
    {
        playerHUD.SetActive(true);
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //LevelSoundManager.instance.bgmSource.Play(); // BGM'yi duraklat

    }

    public void PauseGame()
    {
        playerHUD.SetActive(false);
        pauseMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //LevelSoundManager.instance.bgmSource.Pause(); // BGM'yi duraklat
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
