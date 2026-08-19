using System;
using UnityEngine;
using UnityEngine.UI;

public class CardStagingArea : Singleton<CardStagingArea>
{
    public event Action<CardView> CardStaged;
    [SerializeField] private HandView _handView;
    [SerializeField] private Button _cancelStagingButton;
    [SerializeField] private float _handPositionThreshold;
    [SerializeField] private Transform _discardPileLocation;
    private CardView _stagedCard;

    private void Start()
    {
        CardResolver.Instance.CardResolved += HandleCardResolved;
        _cancelStagingButton.gameObject.SetActive(false);
    }
    public bool IsInPlayableArea(Vector3 cardPosition, Vector3 handPosition)
    {
        return cardPosition.y - handPosition.y > _handPositionThreshold;
    }

    public bool IsStaging()
    {
        return _stagedCard != null;
    }
    public void RequestStaging(CardView newCard)
    {
        if (_stagedCard == null)
        {
            StageCard(newCard);
        }
        else
        {
            ReturnToHand();
            StageCard(newCard);
        }
    }

    private void StageCard(CardView newCard)
    {
        _stagedCard = newCard;
        _cancelStagingButton.gameObject.SetActive(true);

        MoveCardToStaging(newCard);

        CardPileSystem.Instance.RemoveCardFromHand(_stagedCard);
        CardStaged?.Invoke(newCard);
    }
    /// <summary>
    /// Return the current staged card back to the hand
    /// </summary>
    public void ReturnToHand()
    {
        if (!CardPileSystem.Instance.AddToHand(_stagedCard))
            return;

        _stagedCard.SetState(CardState.InHand);
        _stagedCard = null;
        StartCoroutine(_handView.UpdateCardPositions());
    }
    public void CancelStaging()
    {
        if (!IsStaging()) return;

        ReturnToHand();
        _cancelStagingButton.gameObject.SetActive(false);
    }
    private void HandleCardResolved(CardView card)
    {
        _stagedCard = null;
        _cancelStagingButton.gameObject.SetActive(false);

        MoveCardToDiscard(card);

        CardPileSystem.Instance.DiscardPile.AddCard(card);
    }
    private void MoveCardToStaging(CardView Card)
    {
        _stagedCard.SetState(CardState.Staging);
        _stagedCard.CardMovement.MoveTo(transform.position, 0.15f);
        _stagedCard.CardMovement.RotateTo(Quaternion.identity, 0.15f);
    }
    private void MoveCardToDiscard(CardView card)
    {
        card.SetState(CardState.InDiscard);
        card.CardMovement.MoveTo(_discardPileLocation.position, 0.15f);
        card.CardVisual.ChangeScale(0, 0.15f);
    }
    private void OnDisable()
    {
        if (CardResolver.Instance != null)
            CardResolver.Instance.CardResolved -= HandleCardResolved;
    }
}
