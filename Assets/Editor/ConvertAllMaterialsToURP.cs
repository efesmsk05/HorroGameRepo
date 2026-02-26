using UnityEngine;
using UnityEditor;
using System.IO;

public class ConvertAllMaterialsToURP : MonoBehaviour
{
    [MenuItem("Tools/Convert All Materials to URP")]
    static void ConvertAll()
    {
        Shader urpLitShader = Shader.Find("Universal Render Pipeline/Lit");
        if (urpLitShader == null)
        {
            Debug.LogError("URP Lit shader bulunamadý. Lütfen URP yüklü olduðundan emin olun.");
            return;
        }

        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material");
        int convertedCount = 0;

        foreach (string guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat == null || mat.shader == null)
                continue;

            string shaderName = mat.shader.name;

            // Shader ProBuilder içeriyorsa veya Unity'nin Standard shader'ýysa deðiþtir
            if (shaderName.Contains("ProBuilder") || shaderName == "Standard" || shaderName == "Standard (Specular setup)")
            {
                Undo.RecordObject(mat, "Convert Material to URP");

                // Yeni shader'ý ata
                mat.shader = urpLitShader;

                // Mevcut doku ve renkleri korumaya çalýþ
                if (mat.HasProperty("_Color"))
                    mat.SetColor("_BaseColor", mat.GetColor("_Color"));
                if (mat.HasProperty("_MainTex"))
                    mat.SetTexture("_BaseMap", mat.GetTexture("_MainTex"));

                EditorUtility.SetDirty(mat);
                convertedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"{convertedCount} materyal URP Lit shader'a dönüþtürüldü.");
    }
}

