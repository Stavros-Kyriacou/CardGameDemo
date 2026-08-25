using UnityEngine;

public class CardCreator : Singleton<CardCreator>
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private CardData _sampleCard;
    [SerializeField] private float _scaleUpDuration;

    public Card CreateCard(Vector3 position, Quaternion rotation)
    {
        Card card = Instantiate(_cardPrefab, transform.position, transform.rotation);
        card.Initialise(_sampleCard);
        return card;
    }
}