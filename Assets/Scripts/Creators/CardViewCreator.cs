using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView cardViewPrefab;
    public int maxHandSize;
    [SerializeField] private float scaleUpDuration;

    public CardView CreateCardView(Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, transform.position, transform.rotation);
        cardView.transform.localScale = Vector3.zero;
        // cardView.transform.DOScale(Vector3.one, scaleUpDuration);
        return cardView;
    }
}
