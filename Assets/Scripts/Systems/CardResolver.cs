using System;
using System.Collections.Generic;

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

        if (_currentCard.Data.RequiresManualTargeting)
        {
            _targetingSystem.BeginTargeting(_currentCard);
        }
        else
        {
            var cardContext = CreateCardContext(_currentCard, null, BattleManager.Instance.Enemies);
            ResolveCard(cardContext);
        }
    }

    private CardContext CreateCardContext(CardView cardView, List<Enemy> selectedTargets, List<Enemy> availableTargets)
    {
        return new CardContext(cardView, selectedTargets, availableTargets);
    }

    private void HandleTargetsSelected(List<Enemy> targets)
    {
        CardContext context = new CardContext(_currentCard, targets, null);

        ResolveCard(context);
    }
    private void ResolveCard(CardContext context)
    {
        foreach (CardEffect effect in context.CardView.Data.effects)
        {
            effect.Resolve(context);
        }

        CardResolved?.Invoke(_currentCard);
    }
}