using DG.Tweening;
using UnityEngine;

//Move the card in the scene
public class CardMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _handPosition;
    private Quaternion _handRotation;
    public Vector3 HandPosition => _handPosition;
    public Quaternion HandRotation => _handRotation;

    void Start()
    {
        _mainCamera = Camera.main;

    }
    public void MoveTo(Vector3 position, float duration)
    {
        transform.DOMove(position, duration);
    }

    public void RotateTo(Quaternion rotation, float duration)
    {
        transform.DORotate(rotation.eulerAngles, duration);
    }

    public void MoveToMouse(Vector2 screenPosition)
    {
        Vector3 position = _mainCamera.ScreenToWorldPoint(screenPosition);
        position.z = -0.5f;

        transform.position = position;
    }
}
