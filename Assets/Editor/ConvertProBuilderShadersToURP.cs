using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ConvertProBuilderShadersToURP : MonoBehaviour
{
    [MenuItem("Tools/Convert ProBuilder Shaders to URP")]
    static void ConvertShaders()
    {
        // ProBuilder default shader adý (legacy)
        string proBuilderShaderName = "Standard (Specular setup)";

        // URP default Lit shader
        Shader urpLitShader = Shader.Find("Universal Render Pipeline/Lit");
        if (urpLitShader == null)
        {
            Debug.LogError("URP Lit shader bulunamadý. Lütfen URP'nin yüklü olduðundan emin olun.");
            return;
        }

        int convertedCount = 0;

        // Sahnedeki tüm render bileþenlerini al
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Materyallerin üzerinden dön
            Material[] materials = renderer.sharedMaterials;
            bool updated = false;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null && materials[i].shader != null)
                {
                    string shaderName = materials[i].shader.name;

                    // Eski shader ProBuilder'ýn kullandýðýysa deðiþtir
                    if (shaderName.Contains("ProBuilder") || shaderName == proBuilderShaderName)
                    {
                        Material newMat = new Material(urpLitShader);

                        // Renk ve doku gibi özellikleri kopyala
                        if (materials[i].HasProperty("_Color"))
                            newMat.color = materials[i].color;
                        if (materials[i].HasProperty("_MainTex"))
                            newMat.mainTexture = materials[i].mainTexture;

                        materials[i] = newMat;
                        updated = true;
                        convertedCount++;
                    }
                }
            }

            if (updated)
                renderer.sharedMaterials = materials;
        }

        Debug.Log($"{convertedCount} materyal URP Lit shader'a dönüþtürüldü.");
    }
}
