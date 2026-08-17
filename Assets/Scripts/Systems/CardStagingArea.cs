using System;
using UnityEngine;
using UnityEngine.UI;

public class CardStagingArea : Singleton<CardStagingArea>
{
    [SerializeField] private float _handPositionThreshold;
    private CardView _stagedCard;
    [SerializeField] private HandView _handView;
    [SerializeField] private Button _cancelStagingButton;
    private void Start()
    {
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
        _stagedCard.SetState(CardState.Staging);
        _stagedCard.CardMovement.MoveTo(transform.position, 0.15f);
        _stagedCard.CardMovement.RotateTo(Quaternion.identity, 0.15f);
        CardPileSystem.Instance.RemoveCardFromHand(_stagedCard);
        _cancelStagingButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// Return the current staged card back to the hand
    /// </summary>
    public void ReturnToHand()
    {
        if (!CardPileSystem.Instance.AddToHand(_stagedCard))
            return;

        _stagedCard.SetState(CardState.InHand);
        StartCoroutine(_handView.UpdateCardPositions());
    }
    public void CancelStaging()
    {
        if (!IsStaging()) return;

        ReturnToHand();
        _cancelStagingButton.gameObject.SetActive(false);
    }
    public void PlayCard()
    {
        //requires targets?
        //being targeting system
        //return targets
        //play card
        //play card
        //send to discard pile
    }
}
