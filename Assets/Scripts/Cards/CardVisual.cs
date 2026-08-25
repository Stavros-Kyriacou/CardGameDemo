using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

//Updates how the card looks
public class CardVisual : MonoBehaviour
{
    [Header("Card Information")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _manaCostText;
    [SerializeField] private SpriteRenderer _imageSpriteRenderer;

    [Header("Size Scaling")]
    [SerializeField][Range(1.0f, 1.5f)] private float _hoverScaleFactor;
    [SerializeField][Range(0f, 0.5f)] private float _hoverScaleTweenDuration;
    private const float OriginalScale = 1f;

    [Header("Highlighting")]
    [SerializeField] private SpriteRenderer _cardHighlight;


    private CardView _cardView;

    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        SetHighlight(false);
    }
    private void OnEnable()
    {
        _cardView.StateChanged += UpdateVisuals;
    }
    private void OnDisable()
    {
        _cardView.StateChanged -= UpdateVisuals;
    }

    private void UpdateVisuals(CardState newState)
    {
        switch (newState)
        {
            case CardState.InDeck:
                _cardView.transform.localScale = Vector3.zero;
                UpdateDataFields();
                break;
            case CardState.InHand:
                transform.DOScale(OriginalScale, _hoverScaleTweenDuration);
                SetHighlight(false);
                break;
            case CardState.Staging:
                SetHighlight(true);
                break;
        }
    }

    public void SetHovered(bool hovered)
    {
        float scale = hovered ? _hoverScaleFactor : OriginalScale;

        transform.DOScale(scale, _hoverScaleTweenDuration);
    }
    public void SetHighlight(bool enabled)
    {
        _cardHighlight.enabled = enabled;
    }
    public void ChangeScale(float endValue, float duration)
    {
        Vector3 endScale = Vector3.one * endValue;
        transform.DOScale(endScale, duration);
    }
    public void ResetScale()
    {
        transform.DOScale(OriginalScale, _hoverScaleTweenDuration);
    }
    public void UpdateDataFields()
    {
        _nameText.text = _cardView.Data.CardName;
        _descriptionText.text = _cardView.Data.CardDescription;
        _manaCostText.text = _cardView.Data.ManaCost.ToString();
    }
}
