using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView _cardViewPrefab;
    public int maxHandSize;
    [SerializeField] private float _scaleUpDuration;

    public CardView CreateCardView(Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(_cardViewPrefab, transform.position, transform.rotation);
        cardView.SetState(CardState.InDeck);
        cardView.transform.localScale = Vector3.zero;
        // cardView.transform.DOScale(Vector3.one, scaleUpDuration);
        return cardView;
    }
}
