using UnityEngine;

/// <summary>
/// Checks if the card can be staged
/// </summary>
public class CardPlayController : MonoBehaviour
{
    private Card _card;
    private CardInteraction _cardInteraction;
    private CardStagingArea _stagingArea;
    [SerializeField] private float _handPositionThreshold;
    private void Awake()
    {
        _card = GetComponent<Card>();
        _cardInteraction = GetComponent<CardInteraction>();
        _stagingArea = CardStagingArea.Instance;
    }
    public bool ShouldEnterStaging()
    {
        //TODO check mana
        return _stagingArea.IsInPlayableArea(transform.position, _cardInteraction.HandPosition);
    }
    public void EnterStaging()
    {
        _stagingArea.RequestStaging(_card);
    }

    public bool IsInPlayableArea(Vector3 cardPosition, Vector3 handPosition)
    {
        return cardPosition.y - handPosition.y > _handPositionThreshold;
    }
}