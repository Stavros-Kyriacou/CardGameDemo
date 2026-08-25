using System;
using UnityEngine;

//Manage the state of the card
public class Card : MonoBehaviour
{
    private CardMovement _cardMovement;
    private CardInteraction _cardInteration;
    private CardVisual _cardVisual;
    private CardPlayController _cardPlayController;
    private CardState _state;
    private CardData _data;
    
    public CardMovement CardMovement => _cardMovement;
    public CardInteraction CardInteraction => _cardInteration;
    public CardVisual CardVisual => _cardVisual;
    public CardPlayController CardPlayController => _cardPlayController; 
    public CardState State => _state;
    public CardData Data => _data;
    public event Action<CardState> StateChanged;

    void Awake()
    {
        _cardMovement = GetComponent<CardMovement>();
        _cardInteration = GetComponent<CardInteraction>();
        _cardVisual = GetComponent<CardVisual>();
        _cardPlayController = GetComponent<CardPlayController>();
    }
    public void Initialise(CardData data)
    {
        _data = data;
        SetState(CardState.InDeck);
    }
    public void SetState(CardState newState)
    {
        if (State == newState) return;
        _state = newState;
        StateChanged?.Invoke(State);
    }
}