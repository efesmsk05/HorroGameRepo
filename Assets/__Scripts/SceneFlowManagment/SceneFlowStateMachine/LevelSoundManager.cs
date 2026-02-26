using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public static LevelSoundManager instance;

    [Header ("Auido Sources")]
    [SerializeField] public AudioSource bgmSource;
    [SerializeField] public AudioSource sfxSource;

    [Header ("Audio Clips")]
    [SerializeField] private AudioClip levelBGM;//backglound music
    [SerializeField] public AudioClip levelSFX;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(bgmSource != null)
        {
            PlayBGM(levelBGM);
        }

        if (bgmSource !=null && sfxSource != null)
        {
            bgmSource.volume = PlayerPrefs.GetFloat("Volume");

            sfxSource.volume = PlayerPrefs.GetFloat("FxVolume");
        }
    }

    public void PlayFx(AudioClip fx)
    {
        sfxSource.PlayOneShot(fx);
    }

    public void Play3DFx(AudioClip fx, Vector3 position, float volume = 1f, float spatialBlend = 1f)
    {
        GameObject tempGO = new GameObject("Temp3DAudio");
        tempGO.transform.position = position;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = fx;
        aSource.spatialBlend = spatialBlend; // 1 = 3D, 0 = 2D
        aSource.volume = volume;
        aSource.Play();
        Destroy(tempGO, fx.length);
    }
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SaveVolumeSettings(string volumeKey)
    {
        if (bgmSource != null)
        {
            if (volumeKey == "Volume") { bgmSource.volume = PlayerPrefs.GetFloat("Volume"); }
            if (volumeKey == "FxVolume") { sfxSource.volume = PlayerPrefs.GetFloat("FxVolume"); }
        }


    }


}
