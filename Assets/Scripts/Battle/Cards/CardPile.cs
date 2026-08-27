using System.Collections.Generic;

[System.Serializable]
public class CardPile
{
    public List<Card> Cards { get; private set; }
    public CardPile()
    {
        Cards = new List<Card>();
    }
    public void AddCard(Card newCard)
    {
        Cards.Add(newCard);
    }
    public void RemoveCard(Card cardToRemove)
    {
        Cards.Remove(cardToRemove);
    }
    public Card GetFirstCard()
    {
        return Cards[0];
    }
    public int Size()
    {
        return Cards.Count;
    }

    public void ShufflePile()
    {
        //TODO
    }
    public string LogCardsInPile()
    {
        string p = "";
        foreach (var card in Cards)
        {
            p = p + card.name + ", ";
        }
        return p;
    }

}
