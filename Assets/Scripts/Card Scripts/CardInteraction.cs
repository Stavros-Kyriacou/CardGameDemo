using UnityEngine;
using UnityEngine.EventSystems;

//What is the player doing with the card
public class CardInteraction : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private CardView _cardView;
    private CardVisual _cardVisual;
    private CardMovement _cardMovement;
    private CardPlayController _cardPlayController;
    private Vector3 _dragStartPosition;
    private Quaternion _dragStartRotation;
    public Vector3 HandPosition => _dragStartPosition;
    public Quaternion HandRotation => _dragStartRotation;
    
    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        _cardVisual = GetComponent<CardVisual>();
        _cardMovement = GetComponent<CardMovement>();
        _cardPlayController = GetComponent<CardPlayController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // _cardVisual.SetHovered(true);

        if (_cardView.State == CardState.Staging) return;
        _cardView.SetState(CardState.Hovering);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // _cardVisual.SetHovered(false);

        if (_cardView.State == CardState.Staging) return;
        _cardView.SetState(CardState.InHand);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPosition = transform.position;
        _dragStartRotation = transform.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _cardMovement.MoveToMouse(eventData.position);

        bool inPlayableArea = CardStagingArea.Instance.IsInPlayableArea(transform.position, _dragStartPosition);
        _cardVisual.SetHighlight(inPlayableArea);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_cardPlayController.ShouldEnterStaging())
        {
            _cardPlayController.EnterStaging();
        }
        else
        {
            _cardPlayController.ReturnToHand();
        }
    }
}
