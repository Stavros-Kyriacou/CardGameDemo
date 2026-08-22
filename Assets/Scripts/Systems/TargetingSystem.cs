using System;
using System.Collections.Generic;

public class TargetingSystem : Singleton<TargetingSystem>
{
    private BattleManager _battleManager;
    public event Action<List<Enemy>> TargetsSelected;
    private CardView _currentCard;
    private List<Enemy> _targetableEnemies = new List<Enemy>();
    private List<Enemy> _selectedEnemies = new List<Enemy>();

    void Start()
    {
        _battleManager = BattleManager.Instance;
    }

    public void BeginTargeting(CardView card)
    {
        _currentCard = card;
        _targetableEnemies = _battleManager.Enemies;

        switch (card.Data.TargetingRules.TargetingType)
        {
            case TargetingType.None:

                break;
            case TargetingType.Self:

                break;
            case TargetingType.Manual:
                BeginManualTargeting();
                break;
            case TargetingType.AllEnemies:
                TargetsSelected?.Invoke(_targetableEnemies);
                break;
            case TargetingType.RandomEnemy:
                _targetableEnemies = GetRandomEnemy();
                TargetsSelected?.Invoke(_targetableEnemies);
                break;

            default:
                break;
        }

    }
    private void BeginManualTargeting()
    {
        //subscribe to current enemies clicked event
        foreach (var enemy in _targetableEnemies)
        {
            enemy.Clicked += HandleEnemyClicked;
        }

        //TODO: check if enemies are a valid target
        SetEnemiesTargetable(true);
    }

    private void HandleEnemyClicked(Enemy enemy)
    {
        var targetingRules = _currentCard.Data.TargetingRules;

        if (targetingRules.AllowDuplicates)
        {
            _selectedEnemies.Add(enemy);
        }
        else
        {
            //check for duplicates then add
            //or break
        }

        if (_selectedEnemies.Count >= targetingRules.MinTargets)
        {
            //show a button to confirm the selection
        }

        if (_selectedEnemies.Count == targetingRules.MaxTargets)
        {
            FinishManualTargeting();
        }

        //TODO: handle deselecting an enemy and removing from list. make rightclick deselect?
                //avoids issues with cards with multiple targets and allows duplicates
                //player can click the same target multiple times with left click, 
                //or click confirm button if mintargets is met
    }

    private void FinishManualTargeting()
    {
        if (_selectedEnemies == null || _selectedEnemies.Count == 0) return;

        TargetsSelected?.Invoke(_selectedEnemies);

        foreach (var enemy in _targetableEnemies)
        {
            enemy.Clicked -= HandleEnemyClicked;
        }

        SetEnemiesTargetable(false);
        _selectedEnemies = new List<Enemy>();
        _currentCard = null;
    }
    public void SetEnemiesTargetable(bool targetable)
    {
        foreach (var enemy in _targetableEnemies)
        {
            enemy.IsTargetable = targetable;
        }
        HighlightTargetableEnemies(targetable);

    }
    private void HighlightTargetableEnemies(bool highlighted)
    {
        foreach (var enemy in _targetableEnemies)
        {
            enemy.SetHighlight(highlighted);
        }
    }

    private List<Enemy> GetRandomEnemy()
    {
        var targets = new List<Enemy>();
        var randomIndex = UnityEngine.Random.Range(0, _targetableEnemies.Count);
        targets.Add(_targetableEnemies[randomIndex]);
        return targets;
    }
}