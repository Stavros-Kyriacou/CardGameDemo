using System;
using UnityEngine;

public class CardStagingArea : Singleton<CardStagingArea>
{
    public event Action CardStaged;
    [SerializeField] private float _handPositionThreshold;
    private CardView _stagedCard;

    public bool IsInPlayableArea(Vector3 cardPosition, Vector3 handPosition)
    {
        return cardPosition.y - handPosition.y > _handPositionThreshold;
    }
    public bool IsOccupied()
    {
        if (_stagedCard == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void StageCard(CardView newCard)
    {
        if (_stagedCard == null)
        {
            _stagedCard = newCard;
            _stagedCard.SetState(CardState.Staging);
            _stagedCard.CardMovement.MoveTo(transform.position, 0.15f);
            CardPileSystem.Instance.RemoveCardFromHand(_stagedCard);
        }
        else
        {
            Debug.Log("go back home you piggy");
            newCard.CardPlayController.ReturnToHand();
        }
    }
    public void RemoveCard()
    {
        
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
