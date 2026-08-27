using UnityEngine;

public class CardCreator : Singleton<CardCreator>
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private float _scaleUpDuration;

    public Card CreateCard(CardData data)
    {
        Card card = Instantiate(_cardPrefab, transform.position, transform.rotation);
        card.Initialise(data);
        return card;
    }
}