using UnityEngine;

//Handle the actions of moving a card to the staging area from the hand
public class CardPlayController : MonoBehaviour
{
    private CardView _cardView;
    private CardInteraction _cardInteraction;
    private CardMovement _cardMovement;
    private CardStagingArea _stagingArea;
    private void Awake()
    {
        _cardView = GetComponent<CardView>();
        _cardInteraction = GetComponent<CardInteraction>();
        _cardMovement = GetComponent<CardMovement>();
        _stagingArea = CardStagingArea.Instance;
    }
    public bool ShouldEnterStaging()
    {
        //TODO check mana
        return _stagingArea.IsInPlayableArea(transform.position, _cardInteraction.HandPosition);
    }
    public void EnterStaging()
    {
        _stagingArea.StageCard(_cardView);
    }

    public void ReturnToHand()
    {
        _cardMovement.MoveTo(_cardInteraction.HandPosition, 0.15f);
        _cardMovement.RotateTo(_cardInteraction.HandRotation, 0.15f);
        _cardView.SetState(CardState.InHand);
    }

}
