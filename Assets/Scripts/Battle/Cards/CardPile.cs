using System.Collections.Generic;

[System.Serializable]
public class CardPile
{
    public List<Card> Pile { get; private set; }
    public CardPile()
    {
        Pile = new List<Card>();
    }
    public void AddCard(Card newCard)
    {
        Pile.Add(newCard);
    }
    public void RemoveCard(Card cardToRemove)
    {
        Pile.Remove(cardToRemove);
    }
    public Card GetFirstCard()
    {
        return Pile[0];
    }
    public int Size()
    {
        return Pile.Count;
    }

    public void ShufflePile()
    {
        //TODO
    }
    public string LogCardsInPile()
    {
        string p = "";
        foreach (var card in Pile)
        {
            p = p + card.name + ", ";
        }
        return p;
    }

}
