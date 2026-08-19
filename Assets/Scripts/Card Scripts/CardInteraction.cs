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
    private CardStagingArea _stagingArea;
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
    private void Start()
    {
        _stagingArea = CardStagingArea.Instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_cardView.State != CardState.InHand) return;

        _dragStartPosition = transform.position;
        _dragStartRotation = transform.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_cardView.State != CardState.InHand) return;

        _cardMovement.MoveToMouse(eventData.position);

        bool inPlayableArea = _stagingArea.IsInPlayableArea(transform.position, _dragStartPosition);
        _cardVisual.SetHighlight(inPlayableArea);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_cardView.State != CardState.InHand) return;

        if (_cardPlayController.ShouldEnterStaging())
        {
            _cardPlayController.EnterStaging();
        }
        else
        {
            _cardPlayController.ReturnToHand();
        }
        _cardVisual.SetHighlight(false);
    }
}
