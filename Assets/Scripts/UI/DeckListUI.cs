using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckListUI : MonoBehaviour
{
    [SerializeField] private RectTransform _scrollView;
    [SerializeField] private GridLayoutGroup _contentView;
    [SerializeField] private CardUI _cardUIPrefab;

    void Start()
    {
        ToggleDeckList(false);
    }
    public void HandleDeckCreated(CardPile deckPile)
    {
        List<Card> uniqueCards = deckPile.Cards.DistinctBy(x => x.Data.CardName).ToList<Card>();
        var cardQuantities = new int[uniqueCards.Count()];

        for (int i = 0; i < uniqueCards.Count(); i++)
        {
            int count = 0;

            for (int j = 0; j < deckPile.Cards.Count(); j++)
            {
                if (uniqueCards[i].Data.CardName == deckPile.Cards[j].Data.CardName)
                {
                    count++;
                }
            }
            cardQuantities[i] = count;
        }
        
        for (int i = 0; i < uniqueCards.Count(); i++)
        {   
            CardUI cardUI = Instantiate(_cardUIPrefab, _contentView.transform);
            cardUI.Initialise(uniqueCards[i].Data, cardQuantities[i]);
        }
    }
    public void ToggleDeckList(bool visible)
    {
        _scrollView.gameObject.SetActive(visible);
    }
}