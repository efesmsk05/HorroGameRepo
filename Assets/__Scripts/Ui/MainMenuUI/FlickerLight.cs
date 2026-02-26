using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class FlickerLight : MonoBehaviour
{
    public float amplitude = 2f; // Saða-sola hareket mesafesi
    public float frequency = 1f; // Hareketin hýzý

    // Flicl efekti için deðiþkenler
    public Light targetLight;
    public float flickerIntensityMin = 10;
    public float flickerIntensityMax = 35;
    public float flickerSpeed = 20f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        StartCoroutine(FlickerLoop());
    }

    void Update()
    {
        // X ekseninde sinüs dalgasý ile saða-sola hareket
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(offset, 0, 0);
    }

    IEnumerator FlickerLoop()
    {
        while (true) // sonsuz döngü    
        {
            yield return StartCoroutine(FlickerLightEffect()); // Flicker efekti
            if (targetLight != null)
                targetLight.intensity = 40; // Normal yoðunluða dön
            float randomTime = UnityEngine.Random.Range(1f, 6f);

            yield return new WaitForSeconds(randomTime); // 2 saniye bekle, sonra tekrar flicker

        }
    }

    IEnumerator FlickerLightEffect()
    {
        float randomTime = UnityEngine.Random.Range(0.5f, 2f); // Flicker süresi
        float startTime = Time.time;

        while (Time.time - startTime < randomTime)
        {
            if (targetLight != null)
            {
                float flicker = Mathf.Lerp(flickerIntensityMin, flickerIntensityMax, Mathf.PerlinNoise(Time.time * flickerSpeed, 0));
                targetLight.intensity = flicker;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}
