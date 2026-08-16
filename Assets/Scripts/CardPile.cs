using System.Collections.Generic;

[System.Serializable]
public class CardPile
{
    private List<CardView> _pile = new List<CardView>();

    public void AddCard(CardView newCard)
    {
        _pile.Add(newCard);
    }
    public void RemoveCard(CardView cardToRemove)
    {
        _pile.Remove(cardToRemove);
    }
    public CardView GetFirstCard()
    {
        return _pile[0];
    }
    public int Size()
    {
        return _pile.Count;
    }

    public void ShufflePile()
    {
        //TODO
    }
    public string LogCardsInPile()
    {
        string p = "";
        foreach (var card in _pile)
        {
            p = p + card.name + ", ";
        }
        return p;
    }

}
