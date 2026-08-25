using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

//Updates how the card looks
public class CardVisual : MonoBehaviour
{
    [Header("Card Information")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _manaCostText;
    [SerializeField] private SpriteRenderer _imageSpriteRenderer;

    [Header("Hovering")]
    private SortingGroup _sortingGroup;
    [SerializeField] private int _hoveredSortingOrder;
    private const float OriginalScale = 1f;

    [Header("Highlighting")]
    [SerializeField] private SpriteRenderer _cardHighlight;


    private Card _card;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _sortingGroup = GetComponent<SortingGroup>();
        SetHighlight(false);
    }
    private void OnEnable()
    {
        _card.StateChanged += UpdateVisuals;
    }
    private void OnDisable()
    {
        _card.StateChanged -= UpdateVisuals;
    }

    private void UpdateVisuals(CardState newState)
    {
        switch (newState)
        {
            case CardState.InDeck:
                _card.transform.localScale = Vector3.zero;
                UpdateDataFields();
                break;
            case CardState.InHand:
                transform.DOScale(OriginalScale, 0.08f);
                SetHighlight(false);
                break;
            case CardState.Staging:
                SetHighlight(true);
                break;
        }
    }

    public void SetHovered(bool hovered)
    {
        var handPosition = _card.CardMovement.HandPosition;
        var handRotation = _card.CardMovement.HandRotation;

        if (hovered)
        {
            _sortingGroup.sortingOrder = _hoveredSortingOrder;

            var endPos = new Vector3(handPosition.x, -3.6f, -0.5f);
            _card.CardMovement.MoveTo(endPos, 0.12f);
            _card.CardMovement.RotateTo(Quaternion.identity, 0.12f);
        }
        else
        {
            _sortingGroup.sortingOrder = 0;

            _card.CardMovement.ReturnToHandLocation(0.12f);
        }
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
        transform.DOScale(OriginalScale, 0.08f);
    }
    public void UpdateDataFields()
    {
        _nameText.text = _card.Data.CardName;
        _descriptionText.text = _card.Data.CardDescription;
        _manaCostText.text = _card.Data.ManaCost.ToString();
    }
}
