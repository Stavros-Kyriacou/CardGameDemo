using UnityEngine;

//Handle the actions of moving a card to the staging area from the hand
public class CardPlayController : MonoBehaviour
{
    private CardView _cardView;
    private CardInteraction _cardInteraction;
    private CardMovement _cardMovement;
    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        _cardInteraction = GetComponent<CardInteraction>();
        _cardMovement = GetComponent<CardMovement>();
    }
    public bool ShouldEnterStaging()
    {
        //check mana
        //check if card is past staging threshold

        return CardStagingArea.Instance.IsInPlayableArea(transform.position, _cardInteraction.HandPosition);
    }
    public void EnterStaging()
    {
        //remove from hand pile
        //move card to staging area
        _cardMovement.MoveTo(CardStagingArea.Instance.transform.position, 0.15f);
        //redraw hand
        _cardView.SetState(CardState.Staging);
    }

    public void ReturnToHand()
    {
        _cardMovement.MoveTo(_cardInteraction.HandPosition, 0.15f);
        _cardMovement.RotateTo(_cardInteraction.HandRotation, 0.15f);
        _cardView.SetState(CardState.InHand);
    }

}
