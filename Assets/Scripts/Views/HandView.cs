using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;

    private readonly List<CardView> _cards = new List<CardView>();

    public IEnumerator AddCard(CardView cardView)
    {
        _cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }

    private IEnumerator UpdateCardPositions(float duration)
    {
        if (_cards.Count == 0) yield break;
        float cardSpacing = 1f / 10;
        float firstCardPosition = 0.5f - (_cards.Count - 1) * cardSpacing / 2;
        Spline spline = _splineContainer.Spline;
        for (int i = 0; i < _cards.Count; i++)
        {
            float currentCardPosition = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(currentCardPosition); //Spline position in world space
            Vector3 forward = spline.EvaluateTangent(currentCardPosition); 
            Vector3 up = spline.EvaluateUpVector(currentCardPosition);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

            //offset height of each card by small amount in hand so that colliders dont overlap
            _cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            _cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
    public int CardsInHand()
    {
        return _cards.Count;
    }
}
