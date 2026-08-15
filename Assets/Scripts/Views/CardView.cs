using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text manaCostText;
    [SerializeField] private SpriteRenderer imageSpriteRenderer;
    [SerializeField][Range(1.0f, 1.5f)] private float hoverScaleFactor;
    [SerializeField][Range(0f, 0.5f)] private float hoverScaleTweenDuration;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(hoverScaleFactor, hoverScaleTweenDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, hoverScaleTweenDuration);
    }
}
