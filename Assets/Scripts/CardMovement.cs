using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class CardMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField][Range(1.0f, 1.5f)] private float hoverScaleFactor;
    private readonly float originalScaleFactor = 1f;
    [SerializeField][Range(0f, 0.5f)] private float hoverScaleTweenDuration;

    //Card Dragging
    [SerializeField][Range(0f, 0.2f)] private float endDragTweenDuration;
    private Camera mainCamera;
    private readonly float dragCameraDistance = 0;
    private Vector3 dragStartPosition;
    private float dragTimeCount = 0.0f;

    void Start()
    {
        mainCamera = Camera.main;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeScale(hoverScaleFactor);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeScale(originalScaleFactor);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            dragTimeCount += Time.deltaTime;
            if (dragTimeCount > 0.25f)
            {
                dragTimeCount = 0.0f;
            }
        }
        var screenPoint = mainCamera.ScreenToWorldPoint(data.position);
        screenPoint.z = dragCameraDistance;
        transform.position = screenPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.DOMove(dragStartPosition, endDragTweenDuration);
        ChangeScale(originalScaleFactor);
    }
    private void ChangeScale(float scale)
    {
        transform.DOScale(scale, hoverScaleTweenDuration);
    }
}
