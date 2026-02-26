using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flaslightShader : MonoBehaviour
{
    public Light spotlight;
    public Material revealMaterial;

    void Update()
    {
        if (spotlight != null && revealMaterial != null)
        {
            // Materyal'e ýþýk bilgilerini gönder
            revealMaterial.SetVector("_LightPosition", spotlight.transform.position);
            revealMaterial.SetFloat("_LightRange", spotlight.range);

            // El feneri parametrelerini ayarlama
            if (Input.GetKey(KeyCode.Q))
                spotlight.range -= Time.deltaTime * 5f;

            if (Input.GetKey(KeyCode.E))
                spotlight.range += Time.deltaTime * 5f;

            spotlight.range = Mathf.Clamp(spotlight.range, 1f, 20f);

            // Shader deðiþkenini güncelle
            revealMaterial.SetFloat("_LightRange", spotlight.range);
        }
    }
}
