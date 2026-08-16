using UnityEngine;

public class CardStagingArea : Singleton<CardStagingArea>
{
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
    public void StageCard(CardView card)
    {
        if (_stagedCard == null)
        {
            _stagedCard = card;
        }
        else
        {
            //return _stagedCard to hand
            _stagedCard.CardPlayController.ReturnToHand();
            //stage new card
            _stagedCard = card;
        }
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
