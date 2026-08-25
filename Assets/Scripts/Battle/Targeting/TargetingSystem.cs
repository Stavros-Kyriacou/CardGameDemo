using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetingSystem : Singleton<TargetingSystem>
{
    private BattleManager _battleManager;
    public event Action<List<Enemy>> TargetsSelected;
    private Card _currentCard;
    private List<Enemy> _targetableEnemies = new List<Enemy>();
    private List<Enemy> _selectedEnemies = new List<Enemy>();

    void Start()
    {
        _battleManager = BattleManager.Instance;
    }

    public void BeginTargeting(Card card)
    {
        _currentCard = card;
        _targetableEnemies = new List<Enemy>(_battleManager.Enemies);
        BeginManualTargeting();
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

    private void HandleEnemyClicked(Enemy enemy, PointerEventData.InputButton button)
    {
        if (button == PointerEventData.InputButton.Left)
        {
            if (!CanSelectEnemy(enemy))
                return;

            SelectEnemy(enemy);

            if (FinishedTargeting())
            {
                CompleteManualTargeting();
            }
        }

        if (button == PointerEventData.InputButton.Right)
        {
            if (!enemy.IsSelected)
                return;

            if (!_selectedEnemies.Contains(enemy))
                return;

            _selectedEnemies.Remove(enemy);
            enemy.SetSelected(false);
        }
    }

    private bool FinishedTargeting()
    {
        var rules = _currentCard.Data.ManualTargetingRules;

        bool reachedMaxTargets = _selectedEnemies.Count >= rules.MaxTargets;

        bool noMoreUniqueTargetsAvailable =
            !rules.AllowDuplicates &&
            _selectedEnemies.Count == _targetableEnemies.Count;

        return reachedMaxTargets || noMoreUniqueTargetsAvailable;
    }

    private void SelectEnemy(Enemy enemy)
    {
        _selectedEnemies.Add(enemy);
        enemy.SetSelected(true);
    }

    private bool CanSelectEnemy(Enemy enemy)
    {
        var rules = _currentCard.Data.ManualTargetingRules;

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

//TODO: for clicking multiple targets
//show targeting arrows from the card to the target
//show a number above their head to show targeting order

//TODO: create targeting visuals script to handle numbers above targets head
//targeting visuals handles creating and removing targeting arrows
//handles targeting count progress "2/4" shown above the card or on top of the screen
//invoke target selected event and pass through selected targets list + maxTargets (modify for enemies remaining)
//visuals listens for event, and writes "1, 2, 3, 4" in list order. have a sorting box thing so a target can be selected multiple times