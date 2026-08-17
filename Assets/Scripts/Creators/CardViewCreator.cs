using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView _cardViewPrefab;
    [SerializeField] private float _scaleUpDuration;

    public CardView CreateCardView(Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(_cardViewPrefab, transform.position, transform.rotation);
        return cardView;
    }
}