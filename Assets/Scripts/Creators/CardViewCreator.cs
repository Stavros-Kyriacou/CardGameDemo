using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView _cardViewPrefab;
    [SerializeField] private CardData _sampleCard;
    [SerializeField] private float _scaleUpDuration;

    public CardView CreateCardView(Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(_cardViewPrefab, transform.position, transform.rotation);
        cardView.Initialise(_sampleCard);
        return cardView;
    }
}