using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dendee : MonoBehaviour
{
    private TMP_Text tmpText;
    private Tween currentTween;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();

        // Baþlangýçta alpha 0 yap
        tmpText.alpha = 0f;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                FadeToAlpha(1f);
            }
            else
            {
                FadeToAlpha(0f);
            }
        }
        else
        {
            FadeToAlpha(0f);
        }
    }

    void FadeToAlpha(float targetAlpha)
    {
        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        // DOTween TMP Fade
        currentTween = tmpText.DOFade(targetAlpha, 0.5f).SetEase(Ease.InOutSine);
    }
}
