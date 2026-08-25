using DG.Tweening;
using UnityEngine;

public class CardPileSystem : Singleton<CardPileSystem>
{
    public CardPile DeckPile = new CardPile();
    public CardPile HandPile = new CardPile();
    public CardPile DiscardPile = new CardPile();

    [SerializeField] private int _maxDeckSize = 15;
    [SerializeField] private int _maxHandSize = 7;
    [SerializeField] private HandView _handView;

    void Start()
    {
        GenerateDeck();
    }
    public void GenerateDeck()
    {
        for (int i = 0; i < _maxDeckSize; i++)
        {
            Card card = CardCreator.Instance.CreateCard(transform.position, Quaternion.identity);
            DeckPile.AddCard(card);
        }
        ShuffleDeck();
    }
    public void DrawCard()
    {
        if (HandPile.Size() >= _maxHandSize) return;
        if (DeckPile.Size() == 0) return;
        if (CardStagingArea.Instance.IsStaging()) return;

        Card drawnCard = DeckPile.GetFirstCard();
        DeckPile.RemoveCard(drawnCard);

        HandPile.AddCard(drawnCard);
        drawnCard.transform.DOScale(Vector3.one, 0.15f);
        drawnCard.SetState(CardState.InHand);

        StartCoroutine(_handView.UpdateCardPositions());
    }
    public void ShuffleDeck()
    {
        DeckPile.ShufflePile();
    }
    public void RemoveCardFromHand(Card card)
    {
        HandPile.RemoveCard(card);

        if (HandPile.Pile.Count > 0)
        {
            StartCoroutine(_handView.UpdateCardPositions());
        }
    }
    public bool AddToHand(Card card)
    {
        if (HandPile.Size() >= _maxHandSize)
            return false;

        HandPile.AddCard(card);
        return true;
    }
}