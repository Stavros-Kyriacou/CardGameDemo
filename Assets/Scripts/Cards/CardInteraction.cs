using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Check if player interacts with the card. And what to do with that interaction
/// </summary>
public class CardInteraction : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private Card _card;
    private CardVisual _cardVisual;
    private CardMovement _cardMovement;
    private CardPlayController _cardPlayController;
    private Vector3 _handPosition;
    private Quaternion _handRotation;
    public Vector3 HandPosition => _handPosition;
    public Quaternion HandRotation => _handRotation;
    private bool _isDragging;


    private void Awake()
    {
        _card = GetComponent<Card>();
        _cardVisual = GetComponent<CardVisual>();
        _cardMovement = GetComponent<CardMovement>();
        _cardPlayController = GetComponent<CardPlayController>();
    }
    public void SetHandTransform(Vector3 position, Quaternion rotation)
    {
        _handPosition = position;
        _handRotation = rotation;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_card.State != CardState.InHand) return;

        if (_isDragging) return;

        _cardVisual.SetHovered(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_card.State != CardState.InHand) return;

        if (_isDragging) return;

        _cardVisual.SetHovered(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_card.State != CardState.InHand) return;
        
        _isDragging = true;
        _cardMovement.RotateTo(Quaternion.identity, 0.1f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_card.State != CardState.InHand) return;

        _isDragging = true;
        _cardMovement.MoveToMouse(eventData.position);

        bool inPlayableArea = _cardPlayController.IsInPlayableArea(transform.position, _handPosition);
        _cardVisual.SetHighlight(inPlayableArea);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;

        if (_card.State != CardState.InHand) return;

        if (_cardPlayController.ShouldEnterStaging())
        {
            _cardPlayController.EnterStaging();
        }
        else
        {
            _cardMovement.MoveTo(_handPosition, 0.12f);
            _cardMovement.RotateTo(_handRotation, 0.12f);
        }

        _cardVisual.SetHovered(false);
    }
}