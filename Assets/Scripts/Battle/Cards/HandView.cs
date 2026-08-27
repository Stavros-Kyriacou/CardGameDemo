using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _cardMovementDuration = 0.15f;

    public IEnumerator UpdateCardPositions()
    {
        var cardsInHand = CardPileSystem.Instance.HandPile.Cards;

        if (cardsInHand.Count == 0) yield break;
        float cardSpacing = 1f / 10;
        float firstCardPosition = 0.5f - (cardsInHand.Count - 1) * cardSpacing / 2;
        Spline spline = _splineContainer.Spline;
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            float currentCardPosition = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(currentCardPosition); //Spline position in world space
            Vector3 forward = spline.EvaluateTangent(currentCardPosition);
            Vector3 up = spline.EvaluateUpVector(currentCardPosition);
            Quaternion handRotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

            //offset height of each card by small amount in hand so that colliders dont overlap
            var handPosition = splinePosition + transform.position + 0.01f * i * Vector3.back;

            cardsInHand[i].CardInteraction.SetHandTransform(handPosition, handRotation);
            cardsInHand[i].CardMovement.MoveTo(handPosition, _cardMovementDuration);
            cardsInHand[i].CardMovement.RotateTo(handRotation, _cardMovementDuration);
        }
        yield return new WaitForSeconds(_cardMovementDuration);
    }
}
