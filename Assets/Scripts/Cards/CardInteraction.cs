using UnityEngine;
using UnityEngine.EventSystems;

//Detect player input
public class CardInteraction : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private CardStagingArea _stagingArea;
    private Card _card;
    private CardVisual _cardVisual;
    private CardMovement _cardMovement;
    private CardPlayController _cardPlayController;
    private Vector3 _dragStartPosition;
    private Quaternion _dragStartRotation;
    public Vector3 HandPosition => _dragStartPosition;
    public Quaternion HandRotation => _dragStartRotation;
    private bool _isDragging;

    private void Awake()
    {
        _card = GetComponent<Card>();
        _cardVisual = GetComponent<CardVisual>();
        _cardMovement = GetComponent<CardMovement>();
        _cardPlayController = GetComponent<CardPlayController>();
    }
    private void Start()
    {
        _stagingArea = CardStagingArea.Instance;
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

        _dragStartPosition = transform.position;
        _dragStartRotation = transform.rotation;

        _cardMovement.RotateTo(Quaternion.identity, 0.1f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_card.State != CardState.InHand) return;
        _isDragging = true;
        // var dragPosition = new Vector3(eventData.position.x, eventData.position.y, -0.5f);
        _cardMovement.MoveToMouse(eventData.position);

        bool inPlayableArea = _stagingArea.IsInPlayableArea(transform.position, _dragStartPosition);
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
            // _cardPlayController.ReturnToHand();
            _cardMovement.ReturnToHandLocation(0.12f);
        }
        _cardVisual.SetHighlight(false);
    }
}
