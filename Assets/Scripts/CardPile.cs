using System.Collections.Generic;

[System.Serializable]
public class CardPile
{
    private List<CardView> pile = new List<CardView>();

    public void AddCard(CardView newCard)
    {
        pile.Add(newCard);
    }
    public void RemoveCard(CardView cardToRemove)
    {
        pile.Remove(cardToRemove);
    }
    public CardView GetFirstCard()
    {
        return pile[0];
    }
    public int Size()
    {
        return pile.Count;
    }

    public void ShufflePile()
    {
        //TODO
    }
    public string LogCardsInPile()
    {
        string p = "";
        foreach (var card in pile)
        {
            p = p + card.name + ", ";
        }
        return p;
    }

}
