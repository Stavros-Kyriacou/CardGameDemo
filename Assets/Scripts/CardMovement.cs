using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class CardMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;

    [Header("Hover Scale Increase")]
    [SerializeField][Range(1.0f, 1.5f)] private float hoverScaleFactor;
    [SerializeField][Range(0f, 0.5f)] private float hoverScaleTweenDuration;
    private readonly float originalScale = 1f;

    [Header("Dragging")]
    [SerializeField][Range(0f, 0.2f)] private float endDragTweenDuration;
    private readonly float dragCameraDistance = 0;
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
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
        ChangeScale(originalScale);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;
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
        transform.rotation = Quaternion.identity;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.DOMove(dragStartPosition, endDragTweenDuration);
        transform.DORotate(dragStartRotation.eulerAngles, endDragTweenDuration);
        ChangeScale(originalScale);
    }
    private void ChangeScale(float scale)
    {
        transform.DOScale(scale, hoverScaleTweenDuration);
    }
}
