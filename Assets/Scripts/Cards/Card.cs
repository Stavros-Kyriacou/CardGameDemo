using System;
using UnityEngine;

//Manage the state of the card
public class Card : MonoBehaviour
{
    public CardMovement CardMovement { get; private set; }
    public CardInteraction CardInteraction { get; private set; }
    public CardVisual CardVisual { get; private set; }
    public CardPlayController CardPlayController { get; private set; }
    public CardState State { get; private set; }
    public CardData Data { get; private set; }
    public event Action<CardState> StateChanged;

    void Awake()
    {
        CardMovement = GetComponent<CardMovement>();
        CardInteraction = GetComponent<CardInteraction>();
        CardVisual = GetComponent<CardVisual>();
        CardPlayController = GetComponent<CardPlayController>();
    }
    public void Initialise(CardData data)
    {
        Data = data;
        SetState(CardState.InDeck);
    }
    public void SetState(CardState newState)
    {
        if (State == newState) return;
        State = newState;
        StateChanged?.Invoke(State);
    }
}