using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;

public class ButtonHoverMoveX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hareket edecek obje")]
    public Transform targetObject;

    [Header("Animasyon ayarlarý")]
    public float moveOffsetX = 50f; // Ne kadar saða kayacak
    public float duration = 0.3f;
    public Ease ease = Ease.OutExpo;

    private Vector3 originalPosition;
    private Tween moveTween;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target Object atanmadý!");
            return;
        }

        originalPosition = targetObject.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject == null) return;

        moveTween?.Kill();

        Vector3 targetPos = originalPosition + new Vector3(moveOffsetX, 0f, 0f);
        moveTween = targetObject.DOLocalMoveX(targetPos.x, duration).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject == null) return;

        moveTween?.Kill();

        moveTween = targetObject.DOLocalMoveX(originalPosition.x, duration).SetEase(ease);
    }
    public void onClicked()
    {
        Vector3 targetPos = originalPosition + new Vector3(moveOffsetX, 0f, 0f);

        targetObject.localPosition = targetPos;
        moveTween?.Kill();



    }
}
