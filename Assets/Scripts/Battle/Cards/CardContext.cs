using System;
using System.Collections.Generic;

public class CardContext
{
    private Card _card;
    private List<Enemy> _selectedTargets;
    private List<Enemy> _availableTargets;

    public Card Card => _card;
    public List<Enemy> SelectedTargets => _selectedTargets;
    public List<Enemy> AvailableTargets => _availableTargets;
    //TODO: Figure out something for entering null lists
    //think about if its ok to pass in all enemies when not using it for targeting
    public CardContext(Card card, List<Enemy> selectedTargets, List<Enemy> availableTargets)
    {
        _card = card;
        
        if (selectedTargets != null)
        {
            _selectedTargets = new List<Enemy>(selectedTargets);
        }
        else
        {
            _selectedTargets = new List<Enemy>();
        }

        if (availableTargets != null)
        {
            _availableTargets = new List<Enemy>(availableTargets);
        }
        else
        {
            _availableTargets = new List<Enemy>();
        }
    }

    public List<Enemy> GetTargets(TargetingConfig targetingConfig)
    {
        switch (targetingConfig.TargetingType)
        {
            case TargetingType.SingleTarget:
                return GetSelectedTarget(targetingConfig.SelectionIndex);
            case TargetingType.AllTargets:
                return _selectedTargets;
            case TargetingType.AllEnemies:
                return _availableTargets;
            case TargetingType.RandomEnemy:
                return GetRandomTarget();
            default:
                return new List<Enemy>();
        }
    }

    private List<Enemy> GetRandomTarget()
    {
        var targets = new List<Enemy>();
        var randomIndex = UnityEngine.Random.Range(0, _availableTargets.Count);
        targets.Add(_availableTargets[randomIndex]);
        return targets;
    }

    private List<Enemy> GetSelectedTarget(int index)
    {
        if (index < 0 || index >= _selectedTargets.Count)
            return new List<Enemy>();

        return new List<Enemy> { _selectedTargets[index] };
    }
}