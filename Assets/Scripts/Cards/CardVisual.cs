using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Updates how the card looks
/// </summary>
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
    private int _defaultSortingOrder;
    private const float OriginalScale = 1f;

    [Header("Highlighting")]
    [SerializeField] private SpriteRenderer _cardHighlight;


    private Card _card;
    private CardMovement _cardMovement;
    private CardInteraction _cardInteraction;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _cardMovement = GetComponent<CardMovement>();
        _cardInteraction = GetComponent<CardInteraction>();
        _sortingGroup = GetComponent<SortingGroup>();
        _defaultSortingOrder = _sortingGroup.sortingOrder;
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
        if (hovered)
        {
            ChangeSortingOrder(_hoveredSortingOrder);

            var endPos = new Vector3(_cardInteraction.HandPosition.x, -3.6f, -0.5f);
            
            _cardMovement.MoveTo(endPos, 0.12f);
            _cardMovement.RotateTo(Quaternion.identity, 0.12f);
        }
        else
        {
            ChangeSortingOrder(_defaultSortingOrder);

            if (_card.State == CardState.Staging) return;
            _cardMovement.MoveTo(_cardInteraction.HandPosition, 0.12f);
            _cardMovement.RotateTo(_cardInteraction.HandRotation, 0.12f);
        }
    }
    public void ChangeSortingOrder(int order)
    {
        _sortingGroup.sortingOrder = order;
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
