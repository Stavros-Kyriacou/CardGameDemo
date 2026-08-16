using System;
using UnityEngine;

//Manage the stae of the card
public class CardView : MonoBehaviour
{
    public CardMovement CardMovement;
    public CardInteraction CardInteraction;
    public CardVisual CardVisual;
    public CardPlayController CardPlayController;

    public CardState State { get; private set; }
    public event Action<CardState> StateChanged;

    void Awake()
    {
        CardMovement = GetComponent<CardMovement>();
        CardInteraction = GetComponent<CardInteraction>();
        CardVisual = GetComponent<CardVisual>();
        CardPlayController = GetComponent<CardPlayController>();
    }
    public void SetState(CardState newState)
    {
        if (State == newState) return;

        State = newState;
        Debug.Log(State);
        StateChanged?.Invoke(State);

    }
}