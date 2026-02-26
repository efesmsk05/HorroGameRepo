using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource backgroundAudioSource;

    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip backgroundMusic;
    void Start()
    {
        if(mainMenuAudioSource != null && backgroundMusic !=null )
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }

    public void PlayButtonVoice()
    {

        if (mainMenuAudioSource != null)
        {
            if (buttonClickSound != null)
            {
                mainMenuAudioSource.PlayOneShot(buttonClickSound);
            }
            else
            {
                Debug.LogWarning("Button Click Sound is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("Main Menu Audio Source is not assigned.");
        }
    }
}
