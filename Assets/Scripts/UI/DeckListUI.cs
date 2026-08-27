using System;
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
        //TODO: dont let it instantiate in the actual order of the deck. group up cards by type
        //could change CardUI prefab to contain a DuplicatesText tracker instead of 
        //instantiating all copies of a card in the deck
        foreach (var card in deckPile.Cards)
        {
            CardUI cardUI = Instantiate(_cardUIPrefab, _contentView.transform);
            cardUI.Initialise(card.Data);
        }
    }
    public void ToggleDeckList(bool visible)
    {
        _scrollView.gameObject.SetActive(visible);
    }
}
