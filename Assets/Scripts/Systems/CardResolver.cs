using System;
using System.Collections.Generic;
using UnityEngine;

public class CardResolver : Singleton<CardResolver>
{
    private CardStagingArea _cardStagingArea;
    private TargetingSystem _targetingSystem;
    private CardView _currentCard;
    public event Action<CardView> CardResolved;

    private void Start()
    {
        _cardStagingArea = CardStagingArea.Instance;
        _cardStagingArea.CardStaged += HandleCardStaged;

        _targetingSystem = TargetingSystem.Instance;
        _targetingSystem.TargetsSelected += HandleTargetsSelected;

    }
    private void OnDisable()
    {
        _cardStagingArea.CardStaged -= HandleCardStaged;
        _targetingSystem.TargetsSelected -= HandleTargetsSelected;
    }
    private void HandleCardStaged(CardView card)
    {
        _currentCard = card;
        _targetingSystem.BeginTargeting(card);
    }
    private void HandleTargetsSelected(List<Enemy> targets)
    {
        CardContext context = new CardContext
        {
            Card = _currentCard,
            Targets = targets
        };

        ResolveCard(context);
    }
    private void ResolveCard(CardContext context)
    {
        foreach (CardEffect effect in context.Card.Data.effects)
        {
            effect.Resolve(context);
        }

        CardResolved?.Invoke(_currentCard);
    }
}