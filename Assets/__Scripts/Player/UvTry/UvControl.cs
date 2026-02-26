using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UvControl : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private Renderer textRenderer;
    private Color textColor;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float range;
    [SerializeField] LayerMask layerMask;

    [Header ("Uv Text")]
    [SerializeField] float fadeSpeed;
    private byte targetAlpha;
    private byte currentAlhpa;
    private byte maxAlhpa = 255;
    private byte minAlhpa = 0;
    private bool active;

    [Header ("Uv Objects")]
    public float fadeSpeedEnemy = 2f;
    private Renderer lastHitRenderer;
    private float targetAlphaEnemy = 0f;
    private Material currentMat;


    void Update()
    {
        if(FlashlightManager.instance.uvMod == true)
        {
            Raycast();

        }
        else
        {
            if(textMeshPro != null)
            {
                //textMeshPro.gameObject.SetActive(false);
                currentAlhpa = (byte)Mathf.Lerp(currentAlhpa, 0, Time.deltaTime * fadeSpeed );

                FadeText(currentAlhpa,textMeshPro);

            }

            if(currentMat != null)
            {
                StartCoroutine(FadeTo(currentMat, 1f));
                currentMat= null;

            }
        }

    }

    private void Raycast()
    {
        RaycastHit hit;
        Renderer newHitRenderer = null;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,out hit, range, layerMask))
        {


            if (hit.collider.gameObject.GetComponent<TextMeshPro>())// text
            {
                active = true;
                textMeshPro = hit.collider.gameObject.GetComponent<TextMeshPro>();
            }
            else
            {
                newHitRenderer = hit.collider.GetComponent<Renderer>();

                if (newHitRenderer != null)
                {
                    if (newHitRenderer != lastHitRenderer)// yeni deðdiðin objeyle bi önceki deðidiðin obje aynýmý diye kontrol ediyor
                    {
                        // eðer ayný deðilse


                        if (lastHitRenderer != null)// daha önce bir objeye deðdiysen
                            StartCoroutine(FadeTo(lastHitRenderer.material, 1f)); // eski objeyi söndür


                        //deðidiðin yeni objeyi aç
                        currentMat = newHitRenderer.material;
                        StartCoroutine(FadeTo(currentMat, 0f)); 
                        lastHitRenderer = newHitRenderer;
                    }
                    else // kapattýp açtýðýnda  yine ayný objeydesen yani deðidðin objeyle son deðdiðin obje ayný ise
                    {
                        currentMat = newHitRenderer.material;
                        StartCoroutine(FadeTo(currentMat, 0f));
                        lastHitRenderer = newHitRenderer;
                    }
                }

            }


        }
        else // eðer raycast bir objeye deðmediyse
        {
            active =false;
            if (lastHitRenderer != null)
            {
                StartCoroutine(FadeTo(lastHitRenderer.material, 1f));
                lastHitRenderer = null;
            }

        }

        targetAlpha = active ? maxAlhpa : minAlhpa;
        currentAlhpa = (byte)Mathf.Lerp(currentAlhpa, targetAlpha, Time.deltaTime * fadeSpeed );


        FadeText(currentAlhpa , textMeshPro);


    }

    private void FadeText(byte alpha, TextMeshPro text)
    {
        if(textMeshPro != null)
        {
            textMeshPro.ForceMeshUpdate();//update ettik
            var mesh = text.mesh;
            var colors = mesh.colors32;

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].a = alpha;
            }
            mesh.colors32 = colors;

        }


    }
    System.Collections.IEnumerator FadeTo(Material mat, float target)
    {
        float current = mat.GetFloat("_AlphaCliping");

        while (!Mathf.Approximately(current, target))
        {
            current = Mathf.MoveTowards(current, target, Time.deltaTime * fadeSpeedEnemy);
            mat.SetFloat("_AlphaCliping", current);
            yield return null;
        }
    }
}
