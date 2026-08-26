using System;
using UnityEngine;
using UnityEngine.UI;

public class CardStagingArea : Singleton<CardStagingArea>
{
    public event Action<Card> CardStaged;
    [SerializeField] private HandView _handView;
    [SerializeField] private Button _cancelStagingButton;
    [SerializeField] private float _handPositionThreshold;
    [SerializeField] private Transform _discardPileLocation;
    private Card _stagedCard;

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
    public void RequestStaging(Card newCard)
    {
        if (_stagedCard == null)
        {
            StageCard(newCard);
        }
        else
        {
            ReturnStagedCardToHand();
            StageCard(newCard);
        }
    }

    private void StageCard(Card newCard)
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
    public void ReturnStagedCardToHand()
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

        ReturnStagedCardToHand();
        TargetingSystem.Instance.CancelTargeting();
        _cancelStagingButton.gameObject.SetActive(false);
    }
    private void HandleCardResolved(Card card)
    {
        _stagedCard = null;
        _cancelStagingButton.gameObject.SetActive(false);

        MoveCardToDiscard(card);

        CardPileSystem.Instance.DiscardPile.AddCard(card);
    }
    private void MoveCardToStaging(Card Card)
    {
        _stagedCard.SetState(CardState.Staging);
        _stagedCard.CardMovement.MoveTo(transform.position, 0.15f);
        _stagedCard.CardMovement.RotateTo(Quaternion.identity, 0.15f);
    }
    private void MoveCardToDiscard(Card card)
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
