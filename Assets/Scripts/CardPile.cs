using System.Collections.Generic;

[System.Serializable]
public class CardPile
{
    public List<CardView> Pile { get; private set; }
    public CardPile()
    {
        Pile = new List<CardView>();
    }
    public void AddCard(CardView newCard)
    {
        Pile.Add(newCard);
    }
    public void RemoveCard(CardView cardToRemove)
    {
        Pile.Remove(cardToRemove);
    }
    public CardView GetFirstCard()
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
