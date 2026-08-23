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
        _targetableEnemies = new List<Enemy>(_battleManager.Enemies);

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

        if (!CanSelectEnemy(enemy))
            return;

        SelectEnemy(enemy);

        if (FinishedTargeting())
        {
            CompleteManualTargeting();
        }
    }

    private bool FinishedTargeting()
    {
        var rules = _currentCard.Data.TargetingRules;

        //max targets reached
        if (_selectedEnemies.Count == rules.MaxTargets)
            return true;

        //cast when less enemies than maximum but all enemies have been selected
        if (rules.MaxTargets > _targetableEnemies.Count && _selectedEnemies.Count == _targetableEnemies.Count)
            return true;

        return false;
    }

    private void SelectEnemy(Enemy enemy)
    {
        _selectedEnemies.Add(enemy);
        enemy.SetSelected(true);
    }

    private bool CanSelectEnemy(Enemy enemy)
    {
        var rules = _currentCard.Data.TargetingRules;

        //Check for duplicate selection
        if (!rules.AllowDuplicates && _selectedEnemies.Contains(enemy))
            return false;

        //Check for selections full
        if (_selectedEnemies.Count >= rules.MaxTargets)
            return false;

        //Always allow selection if only 1 target required
        if (rules.MaxTargets == 1)
            return true;

        return true;
    }

    private void CompleteManualTargeting()
    {
        if (_selectedEnemies == null || _selectedEnemies.Count == 0) return;

        TargetsSelected?.Invoke(new List<Enemy>(_selectedEnemies));

        foreach (var enemy in _targetableEnemies)
        {
            enemy.Clicked -= HandleEnemyClicked;
            enemy.SetSelected(false);
        }

        SetEnemiesTargetable(false);
        _selectedEnemies.Clear();
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
    public void CancelTargeting()
    {
        _selectedEnemies.Clear();
        HighlightTargetableEnemies(false);
    }

    private List<Enemy> GetRandomEnemy()
    {
        var targets = new List<Enemy>();
        var randomIndex = UnityEngine.Random.Range(0, _targetableEnemies.Count);
        targets.Add(_targetableEnemies[randomIndex]);
        return targets;
    }
}
//TODO: handle deselecting an enemy and removing from list. make rightclick deselect?
//avoids issues with cards with multiple targets and allows duplicates
//player can click the same target multiple times with left click, 
//or click confirm button if mintargets is met


//TODO: for clicking multiple targets
//show targeting arrows from the card to the target
//show a number above their head to show targeting order

//TODO: for all cards that require multiple targets
//require confirmation button to be pressed
//implement and see how it feels
//if its clunky then can bring back automatic casting on maxTargets reached
//i want to let the player change their decision so they can be more thoughtful about critical spells

//TODO: create targeting visuals script to handle numbers above targets head
//targeting visuals handles creating and removing targeting arrows
//handles targeting count progress "2/4"