using DG.Tweening;
using UnityEngine;

public class CardPileSystem : Singleton<CardPileSystem>
{
    public CardPile DeckPile = new CardPile();
    public CardPile HandPile = new CardPile();
    public CardPile DiscardPile = new CardPile();

    [SerializeField] private int maxDeckSize = 15;
    [SerializeField] private int maxHandSize = 7;
    [SerializeField] private HandView handView;

    void Start()
    {
        GenerateDeck();
    }
    public void GenerateDeck()
    {
        for (int i = 0; i < maxDeckSize; i++)
        {
            CardView cardView = CardViewCreator.Instance.CreateCardView(transform.position, Quaternion.identity);
            DeckPile.AddCard(cardView);
        }
        ShuffleDeck();
    }
    public void DrawCard()
    {
        if (HandPile.Size() >= maxHandSize) return;

        var drawnCard = DeckPile.GetFirstCard();
        DeckPile.RemoveCard(drawnCard);

        HandPile.AddCard(drawnCard);
        drawnCard.transform.DOScale(Vector3.one, 0.15f);

        StartCoroutine(handView.AddCard(drawnCard));
    }
    public void ShuffleDeck()
    {
        DeckPile.ShufflePile();
    }
    public void DiscardCard()
    {

    }
    public void DebugPiles()
    {
        Debug.Log("Deck Cards");
        Debug.Log(DeckPile.LogCardsInPile());
        Debug.Log("--------------------------");
        Debug.Log("Hand Cards");
        Debug.Log(HandPile.LogCardsInPile());
        Debug.Log("--------------------------");
        Debug.Log("Discard Cards");
        Debug.Log(DiscardPile.LogCardsInPile());
        Debug.Log("--------------------------");
    }
}
